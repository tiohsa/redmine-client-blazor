using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RedmineClient.Models;
public record IssueCategories
{
    [JsonPropertyName("issue_categories")]
    public List<Category> Categories { get; set; } = new List<Category>();
}

