using Kinde.Api.Hashing;

namespace Kinde.Api.Models.Configuration
{
    public class DefaultConfigurationProvider : IAuthorizationConfigurationProvider
    {
        public DefaultConfigurationProvider(IAuthorizationConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IAuthorizationConfiguration Configuration { get; set; }
        public IAuthorizationConfiguration Get()
        {
            return Configuration;
        }

        public IAuthorizationConfiguration Get(object identifier)
        {
            var name = identifier.ToString();
            switch (name)
            {
                case "PKCE":
                    return new PKCEConfiguration<SHA256CodeVerifier>("reg@live", "openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", null);
                case "Code":
                    return new AuthorizationCodeConfiguration("reg@live", "openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", null);
                default:
                    return new ClientCredentialsConfiguration("reg@live", "openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO");


            }
        }
    }
}
