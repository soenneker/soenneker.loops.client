[![](https://img.shields.io/nuget/v/soenneker.loops.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.loops.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.loops.client/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.loops.client/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.loops.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.loops.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.loops.client/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.loops.client/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Loops.Client

Reuse an authenticated HTTP client for the Loops email API.

## Install

```bash
dotnet add package Soenneker.Loops.Client
```

## Configure and register

```json
{ "Loops": { "ApiKey": "<API key>" } }
```

```csharp
services.AddLoopsHttpClientAsSingleton();
```

The returned client targets `https://app.loops.so/api/v1/` and sends `Authorization: Bearer <API key>`. Use the scoped registration only when each scope should own its transport; provider instances use isolated cache keys.

```csharp
HttpClient client = await loopsHttpClient.Get(cancellationToken);
HttpResponseMessage response = await client.GetAsync("contacts/find?email=person%40example.com", cancellationToken);
response.EnsureSuccessStatusCode();
```

Repeated calls reuse the provider's client. The provider owns it; let the service container dispose the provider.
