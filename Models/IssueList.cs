using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using MudBlazor;


namespace RedmineClient.Models;


public record CreatedIssue
{
    [JsonPropertyName("issue")]
    public Issue Issue { get; set; } = new Issue();
}

public record Issue
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("project")]
    public Project Project { get; set; } = new Project();

    [Required]
    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    [JsonPropertyName("priority")]
    public Priority Priority { get; set; } = new Priority();

    [JsonPropertyName("status")]
    public Status Status { get; set; } = new Status();

    [JsonPropertyName("tracker")]
    public Tracker Tracker { get; set; } = new Tracker();

    [JsonPropertyName("category")]
    public Category? Category { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("start_date")]
    public DateOnly? StartDate { get; set; }

    [JsonPropertyName("due_date")]
    public DateOnly? DueDate { get; set; }

    [JsonIgnore]
    public DateRange? DueDateRange
    {
        get { return new DateRange(StartDate?.ToDateTime(new TimeOnly(0, 0)), DueDate?.ToDateTime(new TimeOnly(0, 0))); }
        set
        {
            if (value is { Start: DateTime start })
            {
                StartDate = DateOnly.FromDateTime(start);
            }
            if (value is { End: DateTime end })
            {
                DueDate = DateOnly.FromDateTime(end);
            }
        }
    }

    [JsonPropertyName("done_ratio")]
    public float? DoneRatio { get; set; }

    [JsonPropertyName("is_private")]
    public bool? IsPrivate { get; set; }

    [JsonPropertyName("estimated_hours")]
    public float? EstimatedHours { get; set; }

    [JsonPropertyName("total_estimated_hours")]
    public float? TotalEstimatedHours { get; set; }

    [JsonPropertyName("spent_hours")]
    public float? SpentHours { get; set; }

    [JsonPropertyName("total_spent_hours")]
    public float? TotalSpentHours { get; set; }

    //[JsonPropertyName("created_on")]
    //public DateOnly? CreatedOn { get; set; }

    [JsonPropertyName("closed_on")]
    public DateOnly? ClosedOn { get; set; }

    public string? baseUrl { get; set; }

    public string TicketUrl()
    {
        return $"{this.baseUrl}/issues/{this.Id}";
    }

}



public record IssueList
{
    [JsonPropertyName("issues")]
    public Issue[] Issues { get; set; } = [];

    public void SetBaseUrl(string baseUrl)
    {
        foreach (var issue in this.Issues)
        {
            issue.baseUrl = baseUrl;
        }
    }


    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("limit")]
    public int limit { get; set; }

}

public record Project
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public record Priority
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
public record Status
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public record Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
public record Tracker
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public record Author
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
