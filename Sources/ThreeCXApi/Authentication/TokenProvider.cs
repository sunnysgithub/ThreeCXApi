namespace ThreeCXApi.Authentication;

internal class TokenProvider
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    
    private readonly TokenApiService _apiService;
    private AccessToken _cachedToken = AccessToken.Empty;

    internal TokenProvider(TokenApiService apiService)
    {
        _apiService = apiService;
    }

    internal async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _semaphore.WaitAsync(cancellationToken);
            
            if (_cachedToken.ExpiresAt > DateTime.UtcNow)
            {
                return _cachedToken.Value;
            }

            _cachedToken = await _apiService.RequestAccessTokenAsync(cancellationToken);

            return _cachedToken.Value;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}