using System.Text.Json.Serialization;

namespace ThreeCXApi.CallControl;

public class Device
{
    [JsonPropertyName("dn")]
    public string Dn { get; set; } = string.Empty;

    [JsonPropertyName("device_id")]
    public string DeviceId { get; set; } = string.Empty;

    [JsonPropertyName("user_agent")]
    public string UserAgent { get; set; } = string.Empty;
} 