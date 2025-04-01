using System.Text.Json.Serialization;

namespace ThreeCXApi.CallControl;

public class ActionResponse
{
    [JsonPropertyName("finalstatus")]
    public string FinalStatus { get; set; } = string.Empty;

    [JsonPropertyName("reason")]
    public string Reason { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public Participant? Result { get; set; }

    [JsonPropertyName("reasontext")]
    public string ReasonText { get; set; } = string.Empty;
} 