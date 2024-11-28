using System.ComponentModel.DataAnnotations;

namespace ThreeCXApi;

internal record ThreeCXApiSettings
{
    internal const string ConfigurationSection = "3CX";

    [Required, Url]
    internal string BaseAddress { get; init; } = string.Empty;

    [Required]
    internal string ClientId { get; init; } = string.Empty;

    [Required]
    internal string ClientSecret { get; init; } = string.Empty;

    internal string GrantType { get; init; } = "client_credentials";
}
