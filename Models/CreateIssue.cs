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

public enum Priorities
{
    Low = 1,
    Normal = 2,
    High = 3,
    Urgent = 4,
    Immediate = 5
}

public enum Categories
{
    ABC = 1,
}

public record CreateIssue
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("parent_issue_id")]
    public int? ParentIssueId { get; set; }

    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("priority_id")]
    public int PriorityId { get; set; } = 2;

    [JsonPropertyName("status_id")]
    public int StatusId { get; set; } = 1;

    [JsonPropertyName("tracker_id")]
    public int TrackerId { get; set; } = 2;

    private int? categoryId;
    [JsonPropertyName("category_id")]
    public int? CategoryId
    {
        get { return categoryId; }
        set
        {
            if (value > 0)
            {
                categoryId = value;
            }
            else
            {
                categoryId = null;
            }
        }
    }

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

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

    [JsonPropertyName("estimated_hours")]
    public float? EstimatedHours { get; set; }

    //   [JsonPropertyName("estimated_hours")]
    //    public float? EstimatedHours { get; set; }

    [JsonIgnore]
    public List<CreateIssue> SubTasks { get; set; } = [];

    [JsonIgnore]
    public Guid? Uuid { get; set; }
}

