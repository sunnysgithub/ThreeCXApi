using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ThreeCXApi.Authentication;
using ThreeCXApi.CallControl;
using ThreeCXApi.Configuration;

namespace ThreeCXApi;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddThreeCXApi(this IServiceCollection services)
    {
        services.AddOptions<ThreeCXApiSettings>()
            .BindConfiguration(ThreeCXApiSettings.ConfigurationSection)
            .ValidateDataAnnotations()
            .Validate(settings => Uri.TryCreate(settings.BaseAddress, UriKind.Absolute, out _), "BaseAddress must be a valid URI");

        services.AddSingleton<TokenProvider>();

        services.AddTransient<TokenDelegatingHandler>();

        services.AddHttpClient<TokenApiService>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<ThreeCXApiSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseAddress);
        });

        services.AddHttpClient<ConfigurationApiService>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<ThreeCXApiSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseAddress);
        }).AddHttpMessageHandler<TokenDelegatingHandler>();

        services.AddHttpClient<CallControlApiService>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<ThreeCXApiSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseAddress);
        }).AddHttpMessageHandler<TokenDelegatingHandler>();

        return services;
    }
}