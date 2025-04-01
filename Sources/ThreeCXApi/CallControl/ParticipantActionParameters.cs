using System.Text.Json.Serialization;

namespace ThreeCXApi.CallControl;

public class ParticipantActionParameters
{
    /// <summary>
    /// The reason for performing the action
    /// </summary>
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// The destination for the action (for divert, routeto, transferto)
    /// </summary>
    [JsonPropertyName("destination")]
    public string? Destination { get; set; }

    /// <summary>
    /// The timeout value for the action
    /// </summary>
    [JsonPropertyName("timeout")]
    public int Timeout { get; set; }

    /// <summary>
    /// Additional optional attached data
    /// </summary>
    [JsonPropertyName("attacheddata")]
    public Dictionary<string, string>? AttachedData { get; set; }
} 