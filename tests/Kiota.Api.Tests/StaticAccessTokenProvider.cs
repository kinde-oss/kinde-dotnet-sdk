using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class StaticAccessTokenProvider : IAuthenticationProvider
{
    private readonly string _accessToken;

    public StaticAccessTokenProvider(string accessToken)
    {
        _accessToken = accessToken;
    }

    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalData = null, CancellationToken cancellationToken = default)
    {
        request.Headers["Authorization"] = new List<string> { $"Bearer {_accessToken}" };
        return Task.CompletedTask;
    }
}
