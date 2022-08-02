using Kinde.Authorization.Models;
using Kinde.Authorization.Models.Configuration;

namespace Kinde.WebExtensions
{
    public class KindeClientFactory : Authorization.Models.Utils.AuthorizationCodeStore<string, KindeClient>
    {
        protected KindeClientFactory()
        {

        }
        private static KindeClientFactory factory;
        public static KindeClientFactory Instance
        {
            get
            {
                if (factory == null)
                {
                    factory = new KindeClientFactory();
                }
                return factory;
            }
        }/// <summary>
        /// Use session Id for instance to persist users authentication
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="clientConfiguration"></param>
        /// <returns></returns>
        public KindeClient GetOrCreate(string instanceId, IClientConfiguration clientConfiguration)
        {
            if (_dictionary.TryGetValue(instanceId, out var cached))
            {
                return cached;
            }
            var client = new Kinde.KindeClient(clientConfiguration, new KindeHttpClient());
            _dictionary.Add(instanceId, client);
            return Get(instanceId);
        }


    }
}