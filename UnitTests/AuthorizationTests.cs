using Kinde;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Newtonsoft.Json;
using UnitTests.Mocks;
using UnitTests.Mocks.Flows;

namespace UnitTests
{
    [TestClass]
    public class Authentication
    {
        protected static string Token { get { return JsonConvert.SerializeObject(new OauthToken() { AccessToken = Guid.NewGuid().ToString(), ExpiresIn = 3600 }); } }

        [TestMethod]
        [DataRow(System.Net.HttpStatusCode.Forbidden, null, true)]
        [DataRow(System.Net.HttpStatusCode.Redirect, null, true)]
        [DataRow(System.Net.HttpStatusCode.OK, null, true)]
        [DataRow(System.Net.HttpStatusCode.OK, "Token", false)]
        public void ClientCredentialsTest(System.Net.HttpStatusCode result, string content, bool throws)
        {
            var response = new HttpResponseMessage(result);

            if (content == "Token")
            {
                response.Content = new StringContent(Token);
            }
            var client = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            IAuthorizationConfiguration authConfig = (IAuthorizationConfiguration)Activator.CreateInstance(typeof(MockClientConfiguration));
            //Act
            if (throws)
            {
                Assert.ThrowsExceptionAsync<ApplicationException>(async () => { await apiClient.Authorize(authConfig); });
                return;
            }
            var task = apiClient.Authorize(authConfig);
            task.Wait();
            //Assert
            Assert.AreEqual(Kinde.Api.Enums.AuthotizationStates.Authorized, apiClient.AuthotizationState);
            Assert.IsNotNull(apiClient.Token);
        }
        [TestMethod]
        [DataRow(System.Net.HttpStatusCode.Forbidden, null, true)]
        [DataRow(System.Net.HttpStatusCode.Redirect, null, false)]
        [DataRow(System.Net.HttpStatusCode.OK, null, true)]
        public void AuthorizationCodeTest(System.Net.HttpStatusCode result, string content, bool throws)
        {
            var response = new HttpResponseMessage(result);

            if (result == System.Net.HttpStatusCode.Redirect)
            {
                response.Headers.Location = new Uri("https://test.com/go/here");
            }
            var client = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            IAuthorizationConfiguration authConfig = (IAuthorizationConfiguration)Activator.CreateInstance(typeof(MockAuthCodeConfiguration));
            //Act
            if (throws)
            {
                Assert.ThrowsExceptionAsync<ApplicationException>(async () => { await apiClient.Authorize(authConfig); });
                return;
            }
            var task = apiClient.Authorize(authConfig);
            task.Wait();
            //Assert
            Assert.AreEqual(Kinde.Api.Enums.AuthotizationStates.UserActionsNeeded, apiClient.AuthotizationState);
            Assert.IsNull(apiClient.Token);
        }
        [TestMethod]
        [DataRow(System.Net.HttpStatusCode.Forbidden, null, true)]
        [DataRow(System.Net.HttpStatusCode.Redirect, null, false)]
        [DataRow(System.Net.HttpStatusCode.OK, null, true)]
        public void PKCETest(System.Net.HttpStatusCode result, string content, bool throws)
        {
            var response = new HttpResponseMessage(result);

            if (result == System.Net.HttpStatusCode.Redirect)
            {
                response.Headers.Location = new Uri("https://test.com/go/here");
            }
            var client = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            MockPKCEConfiguration authConfig = (MockPKCEConfiguration)Activator.CreateInstance(typeof(MockPKCEConfiguration));
            //Act
            if (throws)
            {
                Assert.ThrowsExceptionAsync<ApplicationException>(async () => { await apiClient.Authorize(authConfig); });
                return;
            }
            var task = apiClient.Authorize(authConfig);
            task.Wait();
            //Assert
            Assert.AreEqual(Kinde.Api.Enums.AuthotizationStates.UserActionsNeeded, apiClient.AuthotizationState);
            Assert.IsNull(apiClient.Token);
            Assert.ThrowsException<KeyNotFoundException>(() => { KindeClient.CodeStore.Get(authConfig.State); });
            
        }

    }
}