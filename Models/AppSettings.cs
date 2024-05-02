using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RedmineClient.Models;

public class AppSettings
{
    public enum AppSettingKey
    {
        Url,
        Token,
        ProjectId,
        Width,
        Height,
        X,
        Y,
        AIUrl,
        MetaPrompt,
    };

    [Required]
    public string Url { get; set; } = "http://localhost:8080";

    [Required]
    public string Token { get; set; } = string.Empty;

    [Required]
    public string ProjectId { get; set; } = string.Empty;

    public int? Width { get; set; }

    public int? Height { get; set; }

    public string? AIUrl { get; set; }

    public string? MetaPrompt { get; set; }
}
