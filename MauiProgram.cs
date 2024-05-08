using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using MudBlazor.Services;
using RedmineClient.Models;

namespace RedmineClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddSingleton<PreferencesStoreClone>();
            builder.Services.AddSingleton<SkPhi>();

            builder.Services.AddScoped(sp =>
                new HttpClient
                {
                    DefaultRequestHeaders = {
                        { "X-Redmine-API-Key", sp.GetRequiredService<PreferencesStoreClone>().Get<string>(nameof(AppSettings.AppSettingKey.Token)) ?? "" }
                    },
                    BaseAddress = new Uri(sp.GetRequiredService<PreferencesStoreClone>().Get<string>(nameof(AppSettings.AppSettingKey.Url)) ?? "http://localhost:8080")
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddMudServices();

            return builder.Build();
        }
    }
}
