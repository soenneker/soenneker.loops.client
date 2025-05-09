using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Loops.Client.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Loops.Client.Registrars;

/// <summary>
/// A .NET thread-safe singleton HttpClient for Loops
/// </summary>
public static class LoopsHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="ILoopsHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddLoopsHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<ILoopsHttpClient, LoopsHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ILoopsHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddLoopsHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<ILoopsHttpClient, LoopsHttpClient>();

        return services;
    }
}
