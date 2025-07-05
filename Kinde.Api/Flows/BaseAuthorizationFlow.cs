using Kinde.Api.Client;
using Kinde.Api.Enums;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.User;
using Kinde.Api.Models.Utils;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using System.Web;

namespace Kinde.Api.Flows
{
    public abstract class BaseAuthorizationFlow<TConfig> : IAuthorizationFlow where TConfig : IAuthorizationConfiguration
    {
        protected HttpClient HttpClient { get; set; }
        protected CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();
        public AuthorizationStates AuthorizationState { get; set; }
        public TConfig Configuration { get; private set; }
        public IApplicationConfiguration IdentityProviderConfiguration { get; private set; }
        public OauthToken? Token { get; protected set; } = null!;
        public KindeSSOUser User { get; protected set; }
        public virtual IUserActionResolver UserActionsResolver { get; init; } = new DefaultUserActionResolver();
        public virtual bool RequiresRedirection => true;

        public BaseAuthorizationFlow(IApplicationConfiguration identityProviderConfiguration, TConfig configuration)
        {
            Configuration = configuration;
            IdentityProviderConfiguration = identityProviderConfiguration;
        }

        protected virtual Dictionary<string, string> CreateBaseRequestParameters(bool register = false)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", Configuration.ClientId },
                { "scope", Configuration.Scope }
            };

            if (Configuration.IsCreateOrganization)
            {
                parameters.Add("is_create_org", "true");
            }

            if (!string.IsNullOrEmpty(Configuration.Audience))
            {
                parameters.Add("audience", Configuration.Audience);
            }

            if (!string.IsNullOrEmpty(Configuration.OrganizationId))
            {
                parameters.Add("org_code", Configuration.OrganizationId);
            }

            if (register)
            {
                parameters.Add("start_page", "registration");
                
                if (!string.IsNullOrEmpty(Configuration.PlanInterest))
                {
                    parameters.Add("plan_interest", Configuration.PlanInterest);
                }
                
                if (!string.IsNullOrEmpty(Configuration.PricingTableKey))
                {
                    parameters.Add("pricing_table_key", Configuration.PricingTableKey);
                }
            }

            if (RequiresRedirection)
            {
                parameters.Add("redirect_uri", IdentityProviderConfiguration.ReplyUrl);
            }
            return parameters;
        }

        protected async virtual Task<AuthorizationStates> SendRequest(HttpClient httpClient, Dictionary<string, string> parameters)
        {
            if (RequiresRedirection)
            {
                var url = BuildUrl(IdentityProviderConfiguration.Domain + "/oauth2/auth", parameters);
                await UserActionsResolver.SetLoginUrl(url, ((IRedirectAuthorizationConfiguration)Configuration).State);
                HttpClient = httpClient;
                KindeClient.CodeStore.ItemAdded += CodeStore_ItemAdded;
                AuthorizationState = AuthorizationStates.UserActionsNeeded;
                return AuthorizationState;
            }
            else
            {
                await FetchToken(httpClient, parameters);
                AuthorizationState = AuthorizationStates.Authorized;
                return AuthorizationState;
            }
        }

        protected void CodeStore_ItemAdded(object? sender, ItemAddedEventArgs<string, string> e)
        {
            if (Configuration is IRedirectAuthorizationConfiguration)
            {
                if (e.Key != ((IRedirectAuthorizationConfiguration)Configuration).State) return;
                OnCodeReceived(HttpClient, e.Key, e.Value);
            }
        }

        public abstract void OnCodeReceived(HttpClient httpClient, string key, string value);

        protected virtual string BuildUrl(string baseUrl, Dictionary<string, string> parameters)
        {
            return baseUrl + "?" + string.Join("&", parameters.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value)));
        }

        protected virtual FormUrlEncodedContent BuildContent(Dictionary<string, string> parameters)
        {
            string body = string.Join("&", parameters.Select(x => x.Key + "=" + x.Value));

            return new FormUrlEncodedContent(parameters);
        }

        public void AuthorizeRequest(HttpRequestMessage httpRequestMessage, HttpClient httpClient)
        {
            httpRequestMessage.Headers.TryAddWithoutValidation("cache-control", "no-cache");
            httpRequestMessage.Headers.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.29.2");
            httpRequestMessage.Headers.TryAddWithoutValidation("Authorization", "Bearer " + ExecuteSync(GetOrRefreshToken(httpClient)));
        }

        public virtual Task<AuthorizationStates> Authorize(HttpClient httpClient, bool register = false)
        {
            throw new NotImplementedException("This method MUST be overriden in derived class");
        }

        public virtual async Task Logout(HttpClient httpClient)
        {
            AuthorizationState = AuthorizationStates.NonAuthorized;
            Token = null;
            if (httpClient is KindeHttpClient)
            {
                ((KindeHttpClient)httpClient).Token = null;
            }
            await Task.CompletedTask;
        }

        protected async Task RunRenew(HttpClient client, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay((Token.ExpiresIn - 100) * 1000);
                await RenewInternal(client);
            }
        }

        protected async virtual Task RenewInternal(HttpClient client)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", Configuration.ClientId },
                { "grant_type", "refresh_token" },
                { "refresh_token", Token.RefreshToken },
                { "client_secret", Configuration.ClientSecret }
            };
            await FetchToken(client, parameters);
        }

        public async virtual Task Renew(HttpClient httpClient)
        {
            if (Token == null)
            {
                await Authorize(httpClient);
            }
            else
            {
                await RenewInternal(httpClient);
            }
        }

        protected void SendCode(HttpClient client, Dictionary<string, string> parameters)
        {
            ExecuteSync(FetchToken(client, parameters));
            if (Token != null)
            {
                AuthorizationState = AuthorizationStates.Authorized;
                AddHeader(client, Token);
                User = KindeSSOUser.FromToken(this.Token);
                RunRenew(client, CancellationTokenSource.Token);
            }
        }

        private T ExecuteSync<T>(Task<T> task)
        {
            task.Wait();
            if (task.IsCompletedSuccessfully)
            {
                return task.Result;
            }
            else
            {
                throw task.Exception;
            }
        }

        protected virtual void AddHeader(HttpClient httpClient, OauthToken token)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token?.AccessToken);
        }

        protected async Task<OauthToken> FetchToken(HttpClient httpClient, Dictionary<string, string> parameters)
        {
            var request = BuildContent(parameters);
            request.Headers.TryAddWithoutValidation("Kinde-SDK", $".NET/{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}");

            var response = await httpClient.PostAsync(IdentityProviderConfiguration.Domain + "/oauth2/token", request);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode || string.IsNullOrEmpty(content))
            {
                throw new ApplicationException("Invalid response from server: No token received");
            }

            Token = JsonConvert.DeserializeObject<OauthToken>(content);
            return Token;
        }

        public async Task<string> GetOrRefreshToken(HttpClient httpClient)
        {
            if (Token == null)
            {
                throw new ApplicationException("Please authorize first");
            }

            if (!Token.IsExpired)
            {
                return Token.AccessToken;
            }

            await RenewInternal(httpClient);
            return Token.AccessToken;
        }
    }
}
