using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ThreeCXApi.Authentication;

internal sealed class TokenApiService
{
    private readonly ILogger<TokenApiService> _logger;
    private readonly ThreeCXApiSettings _settings;
    private readonly HttpClient _client;

    internal TokenApiService(
        ILogger<TokenApiService> logger,
        IOptions<ThreeCXApiSettings> settings,
        HttpClient client
    )
    {
        _logger = logger;
        _settings = settings.Value;
        _client = client;
    }

    internal async Task<AccessToken> RequestAccessTokenAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Requesting OAuth2 token for ClientId: {ClientId}", _settings.ClientId);

            var formData = new List<KeyValuePair<string, string>>
            {
                new ("client_id", _settings.ClientId),
                new ("client_secret", _settings.ClientSecret),
                new ("grant_type", _settings.GrantType)
            };

            using var httpResponse = await _client.PostAsync(
                    "/connect/token",
                    new FormUrlEncodedContent(formData),
                    cancellationToken
                );

            httpResponse.EnsureSuccessStatusCode();

            var bearerToken = await httpResponse.Content.ReadFromJsonAsync<RequestAccessTokenResponse>(cancellationToken);
            if (bearerToken is null)
            {
                return AccessToken.Empty;
            }

            return new AccessToken()
            {
                Value = bearerToken.AccessToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(bearerToken.ExpiresIn)
            };

        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("HTTP request failed: {Message}", ex.Message);
        }
        catch (JsonException ex)
        {
            _logger.LogError("JSON deserialization failed: {Message}", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Unexpected error: {Message}", ex.Message);
        }

        return AccessToken.Empty;
    }
}

internal record RequestAccessTokenResponse
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = "Bearer";

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; } = 60;

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

}