using Microsoft.Extensions.Logging;

namespace ThreeCXApi.Configuration;

public class ConfigurationApiService
{
    private readonly HttpClient _client;
    private readonly ILogger<ConfigurationApiService> _logger;

    internal ConfigurationApiService(
        HttpClient client,
        ILogger<ConfigurationApiService> logger
    )
    {
        _client = client;
        _logger = logger;
    }
    
    public async Task<string> Get3CXVersionStringAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var httpResponse = await _client.GetAsync("/xapi/v1/Defs?$select=Id", cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            if(httpResponse.Headers.TryGetValues("X-3CX-Version", out IEnumerable<string>? versions))
            {
                return versions?.FirstOrDefault() ?? string.Empty;
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("HTTP request failed: {Message}", ex.Message);

        }
        catch (Exception ex)
        {
            _logger.LogError("Unexpected error: {Message}", ex.Message);
        }

        return string.Empty;
    }
}