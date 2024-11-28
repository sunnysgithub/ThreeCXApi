namespace ThreeCXApi.Authentication;

internal record AccessToken
{
    internal required string Value { get; init; }
    internal required DateTime ExpiresAt { get; init; }
    
    internal static readonly AccessToken Empty = new ()
    {
        Value = string.Empty,
        ExpiresAt = DateTime.MinValue 
    };
}