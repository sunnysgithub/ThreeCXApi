using System.Text.Json.Serialization;

namespace ThreeCXApi.CallControl;

public class DnState
{
    [JsonPropertyName("dn")]
    public string Dn { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("devices")]
    public List<Device> Devices { get; set; } = new List<Device>();

    [JsonPropertyName("participants")]
    public List<Participant> Participants { get; set; } = new List<Participant>();
} 