using System.Text.Json.Serialization;
namespace Shared.Models;

public class TopReputableByLocation
{
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }
    [JsonPropertyName("reputation")]
    public int? Reputation { get; set; }
    [JsonPropertyName("location")]
    public string? Location { get; set; }
}
