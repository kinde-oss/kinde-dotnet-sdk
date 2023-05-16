using Kinde.Api.Client;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Test.Mocks;
using Kinde.Api.Test.Mocks.Flows;
using Newtonsoft.Json;
using Xunit;

namespace Kinde.Api.Test
{
    [Collection("Sequential")]
    public class CodeTests
    {
        protected static string Token { get { return JsonConvert.SerializeObject(new OauthToken() { AccessToken = Guid.NewGuid().ToString(), ExpiresIn = 3600 }); } }

        public KindeClient client;

        public MockHttpClient _mockClient;

        [Theory]
        [InlineData(typeof(MockAuthCodeConfiguration))]
        [InlineData(typeof(MockPKCEConfiguration))]
        public async Task CodeReceivedTest(Type authType)
        {
            //Arrange
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

            //Act
            KindeClient.OnCodeReceived("here_is_code", authConfig.State);

            //Assert
            Assert.Equal(client, KindeClientFactory.Instance.Get("123"));
            Assert.Equal(Enums.AuthorizationStates.Authorized, client.AuthorizationState);
            Assert.Throws<KeyNotFoundException>(() => { KindeClient.CodeStore.Get("123"); });

            KindeClientFactory.Instance.Remove("123");
        }
    }
}
