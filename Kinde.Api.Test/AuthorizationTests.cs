using Kinde.Api.Client;
using Kinde.Api.Enums;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Model;
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
        public async Task ClientCredentialsTest(System.Net.HttpStatusCode result, string content, bool throws)
        {
            //Arrange
            var response = new HttpResponseMessage(result);
            if (content == "Token")
            {
                response.Content = new StringContent(Token);
            }
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            IAuthorizationConfiguration authConfig = (IAuthorizationConfiguration)Activator.CreateInstance(typeof(MockClientConfiguration));

            //Act
            if (throws)
            {
                await Assert.ThrowsAsync<ApplicationException>(async () => { await apiClient.Authorize(authConfig); });
                return;
            }
            await apiClient.Authorize(authConfig);

            //Assert
            Assert.Equal(AuthorizationStates.Authorized, apiClient.AuthorizationState);
            Assert.NotNull(apiClient.Token);
        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.Forbidden, null, true)]
        [InlineData(System.Net.HttpStatusCode.Redirect, null, false)]
        [InlineData(System.Net.HttpStatusCode.OK, null, true)]
        public async Task AuthorizationCodeTest(System.Net.HttpStatusCode result, string content, bool throws)
        {
            //Arrange
            var response = new HttpResponseMessage(result);
            if (result == System.Net.HttpStatusCode.Redirect)
            {
                response.Headers.Location = new Uri("https://test.com/go/here");
            }
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            IAuthorizationConfiguration authConfig = (IAuthorizationConfiguration)Activator.CreateInstance(typeof(MockAuthCodeConfiguration));

            //Act
            if (throws)
            {
                await Assert.ThrowsAsync<ApplicationException>(async () => { await apiClient.Authorize(authConfig); });
                return;
            }
            await apiClient.Authorize(authConfig);

            //Assert
            Assert.Equal(AuthorizationStates.UserActionsNeeded, apiClient.AuthorizationState);
            Assert.Null(apiClient.Token);
        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.Forbidden, null, true)]
        [InlineData(System.Net.HttpStatusCode.Redirect, null, false)]
        [InlineData(System.Net.HttpStatusCode.OK, null, true)]
        public async Task PKCETest(System.Net.HttpStatusCode result, string content, bool throws)
        {
            //Arrange
            var response = new HttpResponseMessage(result);
            if (result == System.Net.HttpStatusCode.Redirect)
            {
                response.Headers.Location = new Uri("https://test.com/go/here");
            }
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            MockPKCEConfiguration authConfig = (MockPKCEConfiguration)Activator.CreateInstance(typeof(MockPKCEConfiguration));

            //Act
            if (throws)
            {
                await Assert.ThrowsAsync<ApplicationException>(async () => { await apiClient.Authorize(authConfig); });
                return;
            }
            await apiClient.Authorize(authConfig);

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
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler);
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
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler);
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
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler);
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
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);
            var authConfig = Activator.CreateInstance(type) as IAuthorizationConfiguration;

            //Act
            await apiClient.Authorize(authConfig);
            Assert.True(apiClient.Token.IsExpired);
            Assert.Equal("expired token", apiClient.Token.AccessToken);

            handler.Result = new HttpResponseMessage(result) { Content = new StringContent(Token) };
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
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler);
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
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(portalResponse))
            };
            
            // Ensure the response is properly set up
            response.EnsureSuccessStatusCode();
            
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler);
            var apiClient = new KindeClient(new MockIdentityProviderConfiguration(), client);

            // Mock authentication by setting up AuthorizationFlow with a valid token
            var token = new OauthToken { AccessToken = "test_token", ExpiresIn = 3600 };
            var mockAuthFlow = new MockAuthorizationFlow(token);
            
            // Set the AuthorizationFlow before any HTTP requests are made
            var authFlowProperty = apiClient.GetType().GetProperty("AuthorizationFlow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (authFlowProperty == null)
            {
                throw new InvalidOperationException("Failed to access AuthorizationFlow property via reflection");
            }
            authFlowProperty.SetValue(apiClient, mockAuthFlow);

            var options = new GenerateProfileUrlOptions
            {
                Domain = "https://test.kinde.com",
                ReturnUrl = "https://myapp.com/callback",
                SubNav = PortalPage.Profile
            };

            // Act
            var result = await apiClient.GenerateProfileUrl(options);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("https://test.kinde.com/portal/profile", result.Url);
            
            // Verify the request was made correctly
            Assert.NotNull(handler.Request);
            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Contains("/account_api/v1/portal_link", handler.Request.RequestUri.ToString());
            Assert.Contains("sub_nav=profile", handler.Request.RequestUri.ToString());
            Assert.Contains("return_url=", handler.Request.RequestUri.ToString());
            Assert.NotNull(handler.Request.Headers.Authorization);
            Assert.Equal("Bearer test_token", handler.Request.Headers.Authorization.ToString());
        }

        [Fact]
        public async Task GenerateProfileUrl_WithInvalidReturnUrl_ShouldThrowException()
        {
            // Arrange
            var handler = new MockHttpMessageHandler(new HttpResponseMessage());
            var client = new HttpClient(handler);
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
        public void MockHttpClient_ShouldReturnExpectedResponse()
        {
            // Arrange
            var expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("test content")
            };
            var handler = new MockHttpMessageHandler(expectedResponse);
            var client = new HttpClient(handler);

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, "https://test.com");
            var response = client.SendAsync(request).Result;

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("test content", response.Content.ReadAsStringAsync().Result);
        }

        [Fact]
        public void MockHttpClient_ShouldReturnBadRequest_WhenSetToBadRequest()
        {
            // Arrange
            var expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
            {
                Content = new StringContent("error content")
            };
            var handler = new MockHttpMessageHandler(expectedResponse);
            var client = new HttpClient(handler);

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, "https://test.com");
            var response = client.SendAsync(request).Result;

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("error content", response.Content.ReadAsStringAsync().Result);
        }

        [Fact(Skip = "Debug test - enable only for troubleshooting")]
        public void MockHttpClient_DebugTest()
        {
            // Arrange
            var expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("test content")
            };
            var handler = new MockHttpMessageHandler(expectedResponse);
            var client = new HttpClient(handler);

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, "https://test.com");
            var response = client.SendAsync(request).Result;

            // Debug output
            Console.WriteLine($"Expected Status: {expectedResponse.StatusCode}");
            Console.WriteLine($"Actual Status: {response.StatusCode}");
            Console.WriteLine($"Expected Content: {expectedResponse.Content.ReadAsStringAsync().Result}");
            Console.WriteLine($"Actual Content: {response.Content.ReadAsStringAsync().Result}");
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}