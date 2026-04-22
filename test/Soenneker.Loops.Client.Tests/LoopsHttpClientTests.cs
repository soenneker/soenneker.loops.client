using Soenneker.Loops.Client.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Loops.Client.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class LoopsHttpClientTests : HostedUnitTest
{
    private readonly ILoopsHttpClient _httpclient;

    public LoopsHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<ILoopsHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
