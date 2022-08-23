using Kinde.Api.Models;
using Kinde.Api.Models.Configuration;

namespace Kinde
{
    public class KindeClientFactory : Api.Models.Utils.AuthorizationCodeStore<string, KindeClient>
    {
        protected KindeClientFactory()
        {

        }
        private static KindeClientFactory _factory;
        public static KindeClientFactory Instance
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new KindeClientFactory();
                }
                return _factory;
            }
        }
        /// <summary>
        /// Use session Id for instance to persist users authentication
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="identityProviderConfiguration"></param>
        /// <returns></returns>
        public KindeClient GetOrCreate(string instanceId, IIdentityProviderConfiguration identityProviderConfiguration)
        {
            if (_dictionary.TryGetValue(instanceId, out var cached))
            {
                return cached;
            }
            var client = new Kinde.KindeClient(identityProviderConfiguration, new KindeHttpClient());
            _dictionary.Add(instanceId, client);
            return Get(instanceId);
        }


    }
}