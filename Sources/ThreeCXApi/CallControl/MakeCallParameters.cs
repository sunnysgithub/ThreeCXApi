using System.Text.Json.Serialization;

namespace ThreeCXApi.CallControl;

public class MakeCallParameters
{
    /// <summary>
    /// The destination number to call
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; } = string.Empty;

    /// <summary>
    /// Call timeout in seconds
    /// </summary>
    [JsonPropertyName("timeout")]
    public int Timeout { get; set; }

    /// <summary>
    /// Optional attached data
    /// </summary>
    [JsonPropertyName("attacheddata")]
    public Dictionary<string, string>? AttachedData { get; set; }
} 