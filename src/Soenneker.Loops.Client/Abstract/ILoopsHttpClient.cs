using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Loops.Client.Abstract;

/// <summary>
/// Provides a cached HTTP client authenticated for the Loops API.
/// </summary>
public interface ILoopsHttpClient : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the authenticated Loops HTTP client.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
