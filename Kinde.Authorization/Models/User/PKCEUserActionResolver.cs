using Kinde.Authorization.Hashing;
using System.Security.Cryptography;

namespace Kinde.Authorization.Models.User
{
    public class PKCEUserActionResolver<TCodeVerifier> : AuthorizationCodeUserActionResolver, IUserActionResolver where TCodeVerifier : ICodeVerifier<HashAlgorithm>, new()
    {
        protected TCodeVerifier codeVerifier;
        public PKCEUserActionResolver()
        {
            codeVerifier = new TCodeVerifier();
        }
        public override async Task SetCode(string code, string state)
        {
            if (!await codeVerifier.VerifyCode(state, _state)) throw new ApplicationException("Code challenge failed");
            else _code = code;
            OnUserActionsCompleted(code, state);
        }
        public override async Task SetLoginUrl(string url, string state)
        {
            _loginUrl = url;
            _state = await codeVerifier.Compute(state);
            OnUserActionsNeeded(url, state);
        }
    }
}
