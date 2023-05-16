using Kinde.Api.Client;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Test.Mocks;
using Kinde.Api.Test.Mocks.Flows;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace Kinde.Api.Test
{
    [Collection("Sequential")]
    public class Authentication
    {
        protected static string Token { get { return JsonConvert.SerializeObject(new OauthToken() { AccessToken = "access token", ExpiresIn = 3600 }); } }
        protected static string ExpiredToken { get { return JsonConvert.SerializeObject(new OauthToken() { AccessToken = "expired token", RefreshToken = "refresh token", ExpiresIn = 0 }); } }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.Forbidden, null, true)]
        [InlineData(System.Net.HttpStatusCode.Redirect, null, true)]
        [InlineData(System.Net.HttpStatusCode.OK, null, true)]
        [InlineData(System.Net.HttpStatusCode.OK, "Token", false)]
        public void ClientCredentialsTest(System.Net.HttpStatusCode result, string content, bool throws)
        {
            //Arrange
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
                Assert.ThrowsAsync<ApplicationException>(async () => { await apiClient.Authorize(authConfig); });
                return;
            }
            var task = apiClient.Authorize(authConfig);
            task.Wait();

            //Assert
            Assert.Equal(Enums.AuthorizationStates.Authorized, apiClient.AuthorizationState);
            Assert.NotNull(apiClient.Token);
        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.Forbidden, null, true)]
        [InlineData(System.Net.HttpStatusCode.Redirect, null, false)]
        [InlineData(System.Net.HttpStatusCode.OK, null, true)]
        public void AuthorizationCodeTest(System.Net.HttpStatusCode result, string content, bool throws)
        {
            //Arrange
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
                Assert.ThrowsAsync<ApplicationException>(async () => { await apiClient.Authorize(authConfig); });
                return;
            }
            var task = apiClient.Authorize(authConfig);
            task.Wait();

            //Assert
            Assert.Equal(Enums.AuthorizationStates.UserActionsNeeded, apiClient.AuthorizationState);
            Assert.Null(apiClient.Token);
        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.Forbidden, null, true)]
        [InlineData(System.Net.HttpStatusCode.Redirect, null, false)]
        [InlineData(System.Net.HttpStatusCode.OK, null, true)]
        public void PKCETest(System.Net.HttpStatusCode result, string content, bool throws)
        {
            //Arrange
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
                Assert.ThrowsAsync<ApplicationException>(async () => { await apiClient.Authorize(authConfig); });
                return;
            }
            var task = apiClient.Authorize(authConfig);
            task.Wait();

            //Assert
            Assert.Equal(Enums.AuthorizationStates.UserActionsNeeded, apiClient.AuthorizationState);
            Assert.Null(apiClient.Token);
            Assert.Throws<KeyNotFoundException>(() => { KindeClient.CodeStore.Get(authConfig.State); });
        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.OK, typeof(MockClientConfiguration))]
        public async Task FetchToken_AuthorizeSuccess_RequestHeaderShouldContainsSdkVersion(System.Net.HttpStatusCode result, Type type)
        {
            //Arrange
            var response = new HttpResponseMessage(result) { Content = new StringContent(Token) };
            var client = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);
            var authConfig = Activator.CreateInstance(type) as IAuthorizationConfiguration;

            //Act
            await apiClient.Authorize(authConfig);
            var header = client.Request.Content.Headers.FirstOrDefault(x => x.Key == "Kinde-SDK");
            var expectedKindeSdkVersion = $".NET/{FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(KindeClient)).Location).ProductVersion}";

            //Assert
            Assert.NotNull(header.Key);
            Assert.NotNull(header.Value?.FirstOrDefault());
            Assert.Equal(header.Value?.FirstOrDefault(), expectedKindeSdkVersion);
        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.OK, typeof(MockClientConfiguration))]
        public async Task GetToken_UnAuthorizedClient_ThrowException(System.Net.HttpStatusCode result, Type type)
        {
            //Arrange
            var response = new HttpResponseMessage(result) { Content = new StringContent(Token) };
            var client = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);
            var authConfig = Activator.CreateInstance(type) as IAuthorizationConfiguration;

            //Act
            await apiClient.Authorize(authConfig);
            await apiClient.Logout();
            var exception = await Assert.ThrowsAsync<ApplicationException>(async () => { await apiClient.GetToken(); });

            //Assert
            Assert.Equal("Please authorize first", exception.Message);
            Assert.Null(apiClient.Token);
        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.OK, typeof(MockClientConfiguration))]
        public async Task GetToken_UnExpiredToken_ReturnAccessToken(System.Net.HttpStatusCode result, Type type)
        {
            //Arrange
            var response = new HttpResponseMessage(result) { Content = new StringContent(Token) };
            var client = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);
            var authConfig = Activator.CreateInstance(type) as IAuthorizationConfiguration;

            //Act
            await apiClient.Authorize(authConfig);
            var accessToken = await apiClient.GetToken();

            //Assert
            Assert.False(apiClient.Token.IsExpired);
            Assert.Equal("access token", accessToken);
        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.OK, typeof(MockClientConfiguration))]
        public async Task GetToken_ExpiredToken_ReturnNewToken(System.Net.HttpStatusCode result, Type type)
        {
            //Arrange
            var response = new HttpResponseMessage(result) { Content = new StringContent(ExpiredToken) };
            var client = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);
            var authConfig = Activator.CreateInstance(type) as IAuthorizationConfiguration;

            //Act
            await apiClient.Authorize(authConfig);
            Assert.True(apiClient.Token.IsExpired);
            Assert.Equal("expired token", apiClient.Token.AccessToken);

            client.Result = new HttpResponseMessage(result) { Content = new StringContent(Token) };
            var accessToken = await apiClient.GetToken();

            //Assert
            Assert.Equal("access token", accessToken);
            Assert.False(apiClient.Token.IsExpired);
        }
    }
}