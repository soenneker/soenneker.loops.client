using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Extensions.Configuration;
using Soenneker.Loops.Client.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;
using Soenneker.Utils.HttpClientCache.Dtos;

namespace Soenneker.Loops.Client;

///<inheritdoc cref="ILoopsHttpClient"/>
public class LoopsHttpClient : ILoopsHttpClient
{
    private readonly IHttpClientCache _httpClientCache;

    private readonly HttpClientOptions _options;

    public LoopsHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;

        var apiKey = config.GetValueStrict<string>("Loops:ApiKey");

        _options = new HttpClientOptions
        {
            BaseAddress = "https://app.loops.so/api/v1/",
            DefaultRequestHeaders = new Dictionary<string, string>
            {
                {"Authorization", $"Bearer {apiKey}"}
            }
        };
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(LoopsHttpClient), _options, cancellationToken: cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _httpClientCache.RemoveSync(nameof(LoopsHttpClient));
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _httpClientCache.Remove(nameof(LoopsHttpClient));
    }
}
