using System.Text.Json.Serialization;

namespace ThreeCXApi.CallControl;

public class MakeCallParameters
{
    /// <summary>
    /// The extension number to make the call from
    /// </summary>
    [JsonPropertyName("extension")]
    public string Extension { get; set; } = string.Empty;

    /// <summary>
    /// The destination number to call
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; } = string.Empty;

    /// <summary>
    /// Optional: The caller ID to display
    /// </summary>
    [JsonPropertyName("callerId")]
    public string? CallerId { get; set; }

    /// <summary>
    /// Optional: The timeout in seconds before the call is cancelled if not answered
    /// </summary>
    [JsonPropertyName("timeout")]
    public int? Timeout { get; set; }

    /// <summary>
    /// Optional: The destination name to display
    /// </summary>
    [JsonPropertyName("destinationName")]
    public string? DestinationName { get; set; }
} 