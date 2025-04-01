using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
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
    /// Gets the call control state for all DNs
    /// </summary>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>A list of DN states or an empty list in case of an error</returns>
    public async Task<IEnumerable<DnState>> GetCallControlStateAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var httpResponse = await _client.GetAsync("/callcontrol", cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var states = JsonSerializer.Deserialize<IEnumerable<DnState>>(content, _jsonOptions);
            return states ?? Enumerable.Empty<DnState>();
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

        return Enumerable.Empty<DnState>();
    }
    
    /// <summary>
    /// Gets the call control state for a specific DN
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The DN state or null if not found</returns>
    public async Task<DnState?> GetDnStateAsync(string dnNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dnNumber))
        {
            _logger.LogError("DN number must not be empty");
            return null;
        }

        try
        {
            var requestUri = $"/callcontrol/{Uri.EscapeDataString(dnNumber)}";
            var httpResponse = await _client.GetAsync(requestUri, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<DnState>(content, _jsonOptions);
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
    /// Gets the devices associated with a DN
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>A list of devices or an empty list in case of an error</returns>
    public async Task<IEnumerable<Device>> GetDevicesAsync(string dnNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dnNumber))
        {
            _logger.LogError("DN number must not be empty");
            return Enumerable.Empty<Device>();
        }

        try
        {
            var requestUri = $"/callcontrol/{Uri.EscapeDataString(dnNumber)}/devices";
            var httpResponse = await _client.GetAsync(requestUri, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var devices = JsonSerializer.Deserialize<IEnumerable<Device>>(content, _jsonOptions);
            return devices ?? Enumerable.Empty<Device>();
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

        return Enumerable.Empty<Device>();
    }
    
    /// <summary>
    /// Gets a specific device associated with a DN
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="deviceId">The device ID</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The device or null if not found</returns>
    public async Task<Device?> GetDeviceAsync(string dnNumber, string deviceId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dnNumber) || string.IsNullOrEmpty(deviceId))
        {
            _logger.LogError("DN number and device ID must not be empty");
            return null;
        }

        try
        {
            var requestUri = $"/callcontrol/{Uri.EscapeDataString(dnNumber)}/devices/{Uri.EscapeDataString(deviceId)}";
            var httpResponse = await _client.GetAsync(requestUri, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<Device>(content, _jsonOptions);
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
    /// Gets the participants associated with a DN
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>A list of participants or an empty list in case of an error</returns>
    public async Task<IEnumerable<Participant>> GetParticipantsAsync(string dnNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dnNumber))
        {
            _logger.LogError("DN number must not be empty");
            return Enumerable.Empty<Participant>();
        }

        try
        {
            var requestUri = $"/callcontrol/{Uri.EscapeDataString(dnNumber)}/participants";
            var httpResponse = await _client.GetAsync(requestUri, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var participants = JsonSerializer.Deserialize<IEnumerable<Participant>>(content, _jsonOptions);
            return participants ?? Enumerable.Empty<Participant>();
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

        return Enumerable.Empty<Participant>();
    }
    
    /// <summary>
    /// Gets a specific participant associated with a DN
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="participantId">The participant ID</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The participant or null if not found</returns>
    public async Task<Participant?> GetParticipantAsync(string dnNumber, int participantId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dnNumber))
        {
            _logger.LogError("DN number must not be empty");
            return null;
        }

        try
        {
            var requestUri = $"/callcontrol/{Uri.EscapeDataString(dnNumber)}/participants/{participantId}";
            var httpResponse = await _client.GetAsync(requestUri, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<Participant>(content, _jsonOptions);
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
    /// Initiates a call from a DN
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="parameters">The call parameters</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The action response or null on error</returns>
    public async Task<ActionResponse?> MakeCallAsync(string dnNumber, MakeCallParameters parameters, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dnNumber))
        {
            _logger.LogError("DN number must not be empty");
            return null;
        }

        if (parameters == null)
        {
            _logger.LogError("Parameters must not be null");
            return null;
        }

        if (string.IsNullOrEmpty(parameters.Destination))
        {
            _logger.LogError("Destination must not be empty");
            return null;
        }

        try
        {
            var requestUri = $"/callcontrol/{Uri.EscapeDataString(dnNumber)}/makecall";
            var httpResponse = await _client.PostAsJsonAsync(requestUri, parameters, _jsonOptions, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<ActionResponse>(content, _jsonOptions);
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
    
    /// <summary>
    /// Initiates a call from a specific device
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="deviceId">The device ID</param>
    /// <param name="parameters">The call parameters</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The action response or null on error</returns>
    public async Task<ActionResponse?> MakeCallFromDeviceAsync(string dnNumber, string deviceId, MakeCallParameters parameters, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dnNumber) || string.IsNullOrEmpty(deviceId))
        {
            _logger.LogError("DN number and device ID must not be empty");
            return null;
        }

        if (parameters == null)
        {
            _logger.LogError("Parameters must not be null");
            return null;
        }

        if (string.IsNullOrEmpty(parameters.Destination))
        {
            _logger.LogError("Destination must not be empty");
            return null;
        }

        try
        {
            var requestUri = $"/callcontrol/{Uri.EscapeDataString(dnNumber)}/devices/{Uri.EscapeDataString(deviceId)}/makecall";
            var httpResponse = await _client.PostAsJsonAsync(requestUri, parameters, _jsonOptions, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<ActionResponse>(content, _jsonOptions);
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
    
    /// <summary>
    /// Performs an action on a participant
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="participantId">The participant ID</param>
    /// <param name="action">The action to perform</param>
    /// <param name="parameters">The action parameters</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The action response or null on error</returns>
    public async Task<ActionResponse?> PerformParticipantActionAsync(string dnNumber, int participantId, string action, ParticipantActionParameters parameters, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(dnNumber) || string.IsNullOrEmpty(action))
        {
            _logger.LogError("DN number and action must not be empty");
            return null;
        }

        if (parameters == null)
        {
            _logger.LogError("Parameters must not be null");
            return null;
        }

        try
        {
            var requestUri = $"/callcontrol/{Uri.EscapeDataString(dnNumber)}/participants/{participantId}/{action}";
            var httpResponse = await _client.PostAsJsonAsync(requestUri, parameters, _jsonOptions, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<ActionResponse>(content, _jsonOptions);
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
    
    /// <summary>
    /// Drops a participant from a call
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="participantId">The participant ID</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The action response or null on error</returns>
    public async Task<ActionResponse?> DropParticipantAsync(string dnNumber, int participantId, CancellationToken cancellationToken = default)
    {
        var parameters = new ParticipantActionParameters
        {
            Reason = "None"
        };
        
        return await PerformParticipantActionAsync(dnNumber, participantId, "drop", parameters, cancellationToken);
    }
    
    /// <summary>
    /// Answers a call for a participant
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="participantId">The participant ID</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The action response or null on error</returns>
    public async Task<ActionResponse?> AnswerCallAsync(string dnNumber, int participantId, CancellationToken cancellationToken = default)
    {
        var parameters = new ParticipantActionParameters
        {
            Reason = "None"
        };
        
        return await PerformParticipantActionAsync(dnNumber, participantId, "answer", parameters, cancellationToken);
    }
    
    /// <summary>
    /// Transfers a call to another destination
    /// </summary>
    /// <param name="dnNumber">The DN number</param>
    /// <param name="participantId">The participant ID</param>
    /// <param name="destination">The destination to transfer to</param>
    /// <param name="reason">The reason for the transfer</param>
    /// <param name="timeout">The timeout in seconds</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The action response or null on error</returns>
    public async Task<ActionResponse?> TransferCallAsync(string dnNumber, int participantId, string destination, string reason = "None", int timeout = 30, CancellationToken cancellationToken = default)
    {
        var parameters = new ParticipantActionParameters
        {
            Reason = reason,
            Destination = destination,
            Timeout = timeout
        };
        
        return await PerformParticipantActionAsync(dnNumber, participantId, "transferto", parameters, cancellationToken);
    }
} 