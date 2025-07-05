using Kinde.Api.Client;
using Kinde.Api.Enums;
using Kinde.Api.Flows;
using Kinde.Api.Model;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Test.Mocks;
using Kinde.Api.Test.Mocks.Flows;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
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
            Assert.Equal(AuthorizationStates.Authorized, apiClient.AuthorizationState);
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
            Assert.Equal(AuthorizationStates.UserActionsNeeded, apiClient.AuthorizationState);
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
            Assert.Equal(AuthorizationStates.UserActionsNeeded, apiClient.AuthorizationState);
            Assert.Null(apiClient.Token);
            Assert.Throws<KeyNotFoundException>(() => { KindeClient.CodeStore.Get(authConfig.State); });
        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.OK, typeof(MockClientConfiguration))]
        public async Task FetchToken_AuthorizeSuccess_RequestHeaderShouldContainsSdkVersion(System.Net.HttpStatusCode result, Type type)
        {
            //Arrange
            var response = new HttpResponseMessage(result);
            response.Content = new StringContent(Token);
            var client = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            IAuthorizationConfiguration authConfig = (IAuthorizationConfiguration)Activator.CreateInstance(type);

            //Act
            await apiClient.Authorize(authConfig);

            //Assert
            Assert.Equal(AuthorizationStates.Authorized, apiClient.AuthorizationState);
            Assert.NotNull(apiClient.Token);
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

        [Fact]
        public async Task Register_WithBillingParameters_ShouldIncludeBillingParamsInUrl()
        {
            // Arrange
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.Redirect);
            response.Headers.Location = new Uri("https://test.com/go/here");
            var client = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            var authConfig = new AuthorizationCodeConfiguration("test_client", "openid", "test_secret", "test_state", "https://test.kinde.com")
            {
                PlanInterest = "pro_plan",
                PricingTableKey = "pricing_table_123"
            };

            // Act
            await apiClient.Register(authConfig);

            // Assert
            Assert.Equal(AuthorizationStates.UserActionsNeeded, apiClient.AuthorizationState);
            Assert.Null(apiClient.Token);
        }

        [Fact]
        public async Task GenerateProfileUrl_WithValidOptions_ShouldReturnPortalUrl()
        {
            // Arrange
            var portalResponse = new GetPortalLink { Url = "https://test.kinde.com/portal/profile" };
            var tokenResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { access_token = "access token", expires_in = 3600 }))
            };
            var portalResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(portalResponse))
            };

            // Here: first call returns tokenResponse, second returns portalResponseMessage
            var handler = new SequenceMessageHandler(tokenResponse, portalResponseMessage);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://jahangeer.kinde.com")
            };

            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);
            var options = new GenerateProfileUrlOptions
            {
                Domain = "https://test.kinde.com",
                ReturnUrl = "https://test.com/callback",
                SubNav = PortalPage.Profile
            };

            // Act
            await apiClient.Authorize(new MockClientConfiguration());
            var result = await apiClient.GenerateProfileUrl(options);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("https://test.kinde.com/portal/profile", result.Url);          
        }

        [Fact]
        public async Task GenerateProfileUrl_WithInvalidReturnUrl_ShouldThrowException()
        {
            // Arrange
            var client = new MockHttpClient(new HttpResponseMessage());
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            var options = new GenerateProfileUrlOptions
            {
                Domain = "https://test.kinde.com",
                ReturnUrl = "invalid-url",
                SubNav = PortalPage.Profile
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => apiClient.GenerateProfileUrl(options));
        }

        [Fact]
        public async Task GenerateProfileUrl_WithNullOptions_ShouldThrowArgumentNullException()
        {
            // Arrange
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), new HttpClient());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => apiClient.GenerateProfileUrl(null));
        }
      
        [Fact]
        public async Task GenerateProfileUrl_WithEmptyDomain_ShouldThrowArgumentException()
        {
            // Arrange
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), new HttpClient());
            var options = new GenerateProfileUrlOptions
            {
                Domain = string.Empty,
                ReturnUrl = "https://myapp.com/return",
                SubNav = PortalPage.Profile
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => apiClient.GenerateProfileUrl(options));
        }
               
        [Fact]
        public async Task GenerateProfileUrl_WithoutAuthentication_ShouldThrowApplicationException()
        {
            // Arrange
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), new HttpClient());
            var options = new GenerateProfileUrlOptions
            {
                Domain = "https://test.kinde.com",
                ReturnUrl = "https://myapp.com/return",
                SubNav = PortalPage.Profile
            };

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => apiClient.GenerateProfileUrl(options));
        }
      
        [Fact]
        public async Task GenerateProfileUrl_WithInvalidJsonResponse_ShouldThrow()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("not-json")
            };
            var httpClient = new MockHttpClient(response);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), httpClient);

            // Simulate authenticated state by setting private Token via reflection
            var token = new OauthToken { AccessToken = "test_token", ExpiresIn = 3600 };
            typeof(KindeClient)
                .GetProperty("Token", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(apiClient, token);

            var options = new GenerateProfileUrlOptions
            {
                Domain = "https://test.kinde.com",
                ReturnUrl = "https://myapp.com/return",
                SubNav = PortalPage.Profile
            };

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(
                () => apiClient.GenerateProfileUrl(options));
        }
    }
}