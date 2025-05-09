using Soenneker.Loops.Client.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Loops.Client.Tests;

[Collection("Collection")]
public class LoopsHttpClientTests : FixturedUnitTest
{
    private readonly ILoopsHttpClient _httpclient;

    public LoopsHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<ILoopsHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
