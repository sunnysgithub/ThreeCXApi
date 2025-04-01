using System.Text.Json.Serialization;

namespace ThreeCXApi.CallControl;

public class Participant
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("dn")]
    public string Dn { get; set; } = string.Empty;

    [JsonPropertyName("party_caller_name")]
    public string PartyCallerName { get; set; } = string.Empty;

    [JsonPropertyName("party_dn")]
    public string PartyDn { get; set; } = string.Empty;

    [JsonPropertyName("party_caller_id")]
    public string PartyCallerId { get; set; } = string.Empty;

    [JsonPropertyName("party_did")]
    public string PartyDid { get; set; } = string.Empty;

    [JsonPropertyName("device_id")]
    public string DeviceId { get; set; } = string.Empty;

    [JsonPropertyName("party_dn_type")]
    public string PartyDnType { get; set; } = string.Empty;

    [JsonPropertyName("direct_control")]
    public bool DirectControl { get; set; }

    [JsonPropertyName("originated_by_dn")]
    public string OriginatedByDn { get; set; } = string.Empty;

    [JsonPropertyName("originated_by_type")]
    public string OriginatedByType { get; set; } = string.Empty;

    [JsonPropertyName("referred_by_dn")]
    public string ReferredByDn { get; set; } = string.Empty;

    [JsonPropertyName("referred_by_type")]
    public string ReferredByType { get; set; } = string.Empty;

    [JsonPropertyName("on_behalf_of_dn")]
    public string OnBehalfOfDn { get; set; } = string.Empty;

    [JsonPropertyName("on_behalf_of_type")]
    public string OnBehalfOfType { get; set; } = string.Empty;

    [JsonPropertyName("callid")]
    public int CallId { get; set; }

    [JsonPropertyName("legid")]
    public int LegId { get; set; }
} 