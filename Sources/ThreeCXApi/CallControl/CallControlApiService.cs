using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ThreeCXApi.CallControl;

public class CallControlApiService
{
    private readonly HttpClient _client;
    private readonly ILogger<CallControlApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    internal CallControlApiService(
        HttpClient client,
        ILogger<CallControlApiService> logger
    )
    {
        _client = client;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    
    /// <summary>
    /// Gets the status of a specific call
    /// </summary>
    /// <param name="callId">The ID of the call</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The status of the call or null if the call was not found</returns>
    public async Task<CallStatus?> GetCallStatusAsync(string callId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(callId))
        {
            _logger.LogError("CallId must not be empty");
            return null;
        }

        try
        {
            var requestUri = $"/api/CallControl/GetCallInfo?callid={Uri.EscapeDataString(callId)}";
            
            using var httpResponse = await _client.GetAsync(requestUri, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<CallStatus>(content, _jsonOptions);
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

        return null;
    }
    
    /// <summary>
    /// Gets the status of all active calls
    /// </summary>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>A list of active calls or an empty list in case of an error</returns>
    public async Task<IEnumerable<CallStatus>> GetActiveCallsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var httpResponse = await _client.GetAsync("/api/CallControl/GetActiveCalls", cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var calls = JsonSerializer.Deserialize<IEnumerable<CallStatus>>(content, _jsonOptions);
            return calls ?? Enumerable.Empty<CallStatus>();
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

        return Enumerable.Empty<CallStatus>();
    }

    /// <summary>
    /// Initiates a new call with the specified parameters
    /// </summary>
    /// <param name="parameters">The parameters for making the call</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The call ID if successful, null otherwise</returns>
    public async Task<string?> MakeCallAsync(MakeCallParameters parameters, CancellationToken cancellationToken = default)
    {
        if (parameters == null)
        {
            _logger.LogError("Parameters must not be null");
            return null;
        }

        if (string.IsNullOrEmpty(parameters.Extension))
        {
            _logger.LogError("Extension must not be empty");
            return null;
        }

        if (string.IsNullOrEmpty(parameters.Destination))
        {
            _logger.LogError("Destination must not be empty");
            return null;
        }

        try
        {
            var json = JsonSerializer.Serialize(parameters, _jsonOptions);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            using var httpResponse = await _client.PostAsync("/api/CallControl/MakeCall", content, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var response = JsonSerializer.Deserialize<MakeCallResponse>(responseContent, _jsonOptions);
            return response?.CallId;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("HTTP request failed: {Message}", ex.Message);
        }
        catch (JsonException ex)
        {
            _logger.LogError("JSON serialization/deserialization failed: {Message}", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Unexpected error: {Message}", ex.Message);
        }

        return null;
    }
}

internal class MakeCallResponse
{
    [JsonPropertyName("callId")]
    public string? CallId { get; set; }
} 