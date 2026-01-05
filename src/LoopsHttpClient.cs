using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Loops.Client.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Loops.Client;

/// <inheritdoc cref="ILoopsHttpClient"/>
public sealed class LoopsHttpClient : ILoopsHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;

    private static readonly Uri _prodBaseUrl = new("https://app.loops.so/api/v1/", UriKind.Absolute);

    private const string _clientId = nameof(LoopsHttpClient);

    public LoopsHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        // No closure: state passed explicitly + static lambda
        return _httpClientCache.Get(_clientId, (config: _config, prodBaseUrl: _prodBaseUrl), static state =>
        {
            var apiKey = state.config.GetValueStrict<string>("Loops:ApiKey");

            return new HttpClientOptions
            {
                BaseAddress = state.prodBaseUrl,
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {"Authorization", $"Bearer {apiKey}"}
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_clientId);
    }
}