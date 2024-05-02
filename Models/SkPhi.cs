#pragma warning disable SKEXP0010
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using System.Text;

namespace RedmineClient.Models;
public class SkPhi
{

    //Kernel kernel;
    ILogger<SkPhi> logger;
    public static string URL = "http://localhost:11434";
    public static string SK_PROMPT = @"ChatBot can have a conversation with you about any topic.
It can give explicit instructions or say 'I don't know' if it does not have an answer.

{{$history}}
User: {{$userInput}}
ChatBot:";

    public SkPhi(ILogger<SkPhi> logger)
    {
        this.logger = logger;
    }


    public async Task<string> Prompt(string userInput, string url, string skPrompt)
    {
        var endpoint = new Uri(url ?? URL);
        var modelId = "phi3";

        var kernelBuilder = Kernel.CreateBuilder().AddOpenAIChatCompletion(modelId: modelId, endpoint: endpoint, apiKey: null);
        var kernel = kernelBuilder.Build();

        // const string skPrompt = SK_PROMPT;
        var chatFunction = kernel.CreateFunctionFromPrompt(skPrompt ?? SK_PROMPT);
        var history = "";
        var arguments = new KernelArguments()
        {
            ["history"] = history
        };

        arguments["userInput"] = userInput;

        logger.LogInformation(arguments.ToString());

        var bot_answer = await chatFunction.InvokeAsync(kernel, arguments);

        history += $"\nUser: {userInput}\nAI: {bot_answer}\n";
        arguments["history"] = history;

        logger.LogInformation($"{bot_answer}");
        return $"{bot_answer}";
    }
}
