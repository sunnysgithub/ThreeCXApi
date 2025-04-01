using System.Text.Json.Serialization;

namespace ThreeCXApi.CallControl;

public class CallStatus
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonPropertyName("callstate")]
    public string CallState { get; set; } = string.Empty;
    
    [JsonPropertyName("partyA")]
    public PartyInfo PartyA { get; set; } = new();
    
    [JsonPropertyName("partyB")]
    public PartyInfo PartyB { get; set; } = new();
    
    [JsonPropertyName("startTimeUtc")]
    public DateTime? StartTimeUtc { get; set; }
    
    [JsonPropertyName("answerTimeUtc")]
    public DateTime? AnswerTimeUtc { get; set; }
    
    [JsonPropertyName("endTimeUtc")]
    public DateTime? EndTimeUtc { get; set; }
    
    [JsonPropertyName("duration")]
    public int Duration { get; set; }
}

public class PartyInfo
{
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("dn")]
    public string Dn { get; set; } = string.Empty;
} 