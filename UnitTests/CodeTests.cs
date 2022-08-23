using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kinde;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Newtonsoft.Json;
using UnitTests.Mocks;
using UnitTests.Mocks.Flows;

namespace UnitTests
{
    [TestClass]
    public class CodeTests
    {
        protected static string Token { get { return JsonConvert.SerializeObject(new OauthToken() { AccessToken = Guid.NewGuid().ToString(), ExpiresIn = 3600 }); } }


        public KindeClient client;

        public  MockHttpClient _mockClient;
        [TestMethod]
        [DataRow(typeof(MockAuthCodeConfiguration))]
        [DataRow(typeof(MockPKCEConfiguration))]
        public async Task CodeRecieveTest(Type authType)
        {
            IRedirectAuthorizationConfiguration authConfig = (IRedirectAuthorizationConfiguration)Activator.CreateInstance(authType);

            var config = new MockIdentityProviderConfiguration();
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.Redirect);
            response.Headers.Location = new Uri("https://test.tes");
            _mockClient = new MockHttpClient(response);
            client = KindeClientFactory.Instance.GetOrCreate("123", config, _mockClient);

            await client.Authorize((IAuthorizationConfiguration)authConfig);

            var resp = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            resp.Content = new StringContent(Token);
            _mockClient.Result = resp;
           
            KindeClient.OnCodeRecieved("here_is_code", authConfig.State);
            Assert.AreEqual(client, KindeClientFactory.Instance.Get("123"));
            Assert.AreEqual(Kinde.Api.Enums.AuthotizationStates.Authorized, client.AuthotizationState);
            Assert.ThrowsException<KeyNotFoundException>(()=> { KindeClient.CodeStore.Get("123"); });

            KindeClientFactory.Instance.Remove("123");
        }

    }
}
