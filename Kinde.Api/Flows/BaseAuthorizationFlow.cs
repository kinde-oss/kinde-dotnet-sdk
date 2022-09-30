using Kinde.Api.Enums;
using Kinde.Api.Models;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.User;
using Newtonsoft.Json;
using System.Web;

namespace Kinde.Api.Flows
{
    public abstract class BaseAuthorizationFlow<TConfig> : IAuthorizationFlow where TConfig : IAuthorizationConfiguration
    {
        protected HttpClient _httpClient;
        public AuthotizationStates AuthotizationState { get; set; }
        public TConfig Configuration { get; private set; }
        public IApplicationConfiguration IdentityProviderConfiguration { get; private set; }
        public OauthToken? Token { get; private set; } = null!;

        public virtual IUserActionResolver UserActionsResolver { get; init; } = new DefaultUserActionResolver();

        public virtual bool RequiresRedirection => true;
        protected CancellationTokenSource CancellationTokenSorce = new CancellationTokenSource();
        public BaseAuthorizationFlow(IApplicationConfiguration identityProviderConfiguration, TConfig configuration)
        {
            Configuration = configuration;
            IdentityProviderConfiguration = identityProviderConfiguration;
        }
        protected virtual Dictionary<string, string> CreateBaseRequestParameters(bool register = false)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("client_id", Configuration.ClientId);
            parameters.Add("client_secret", Configuration.ClientSecret);
            parameters.Add("scope", Configuration.Scope);
            if (register) parameters.Add("start_page", "registration");
            if (RequiresRedirection) parameters.Add("redirect_uri", IdentityProviderConfiguration.ReplyUrl);
            return parameters;
        }

        protected async virtual Task<AuthotizationStates> SendRequest(HttpClient httpClient, Dictionary<string, string> parameters)
        {
            if (RequiresRedirection)
            {
                var response = await httpClient.PostAsync(IdentityProviderConfiguration.Domain + "/oauth2/auth", BuildContent(parameters));
                if (response.Headers.Location != null)
                {
                    await UserActionsResolver.SetLoginUrl(IdentityProviderConfiguration.Domain + response.Headers.Location.ToString(), ((IRedirectAuthorizationConfiguration)Configuration).State);
                    _httpClient = httpClient;
                    KindeClient.CodeStore.ItemAdded += CodeStore_ItemAdded;
                    AuthotizationState = AuthotizationStates.UserActionsNeeded;
                    return AuthotizationState;
                }
                else
                {
                    throw new ApplicationException(await response.Content.ReadAsStringAsync());
                }
            }
            else
            {
                var response = await httpClient.PostAsync(IdentityProviderConfiguration.Domain + "/oauth2/token", BuildContent(parameters));// BuildContent(parameters) );
                var tokenString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(tokenString)) throw new ApplicationException("Invalid response from server: No token recieved");
                Token = JsonConvert.DeserializeObject<OauthToken>(tokenString);
                AuthotizationState = AuthotizationStates.Authorized;
                return AuthotizationState;
            }
        }

        protected void CodeStore_ItemAdded(object? sender, Models.Utils.ItemAddedEventArgs<string, string> e)
        {
            if (Configuration is IRedirectAuthorizationConfiguration)
            {
                if (e.Key != ((IRedirectAuthorizationConfiguration)Configuration).State) return;
                OnCodeRecieved(_httpClient, e.Key, e.Value);

            }

        }

        public abstract void OnCodeRecieved(HttpClient httpClient, string key, string value);
        protected virtual string BuildUrl(string baseUrl, Dictionary<string, string> parameters)
        {
            return baseUrl + "?" + string.Join("&", parameters.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value)));
        }
        protected virtual FormUrlEncodedContent BuildContent(Dictionary<string, string> parameters)
        {
            string body = string.Join("&", parameters.Select(x => x.Key + "=" + x.Value));

            return new FormUrlEncodedContent(parameters);
        }
        public void AuthorizeRequest(HttpRequestMessage httpRequestMessage)
        {
            httpRequestMessage.Headers.TryAddWithoutValidation("cache-control", "no-cache");
            httpRequestMessage.Headers.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.29.2");
            if (Token != null)
            {
                httpRequestMessage.Headers.TryAddWithoutValidation("Authorization", "Bearer " + Token.AccessToken);
            }

        }

        public virtual Task<AuthotizationStates> Authorize(HttpClient httpClient, bool register = false)
        {
            throw new NotImplementedException("This method MUST be overriden in derived class");
        }

        public virtual async Task Logout(HttpClient httpClient)
        {
            AuthotizationState = AuthotizationStates.NonAuthorized;
            Token = null;
            if(httpClient is KindeHttpClient)
            {
                ((KindeHttpClient)httpClient).Token = null;
               // httpClient.DefaultRequestHeaders.Remove()
            }
           
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
            var parameters = new Dictionary<string, string>();
            parameters.Add("client_id",Configuration.ClientId);
            parameters.Add("grant_type", "refresh_token");
            parameters.Add("refresh_token", Token.RefreshToken);
            parameters.Add("client_secret", Configuration.ClientSecret);
            var response = await client.PostAsync(IdentityProviderConfiguration.Domain + "/oauth2/token", BuildContent(parameters));
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Token = JsonConvert.DeserializeObject<OauthToken>(content);
            }
            else
            {
                throw new ApplicationException(content);
            }
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

            var response = ExecuteSync(client.PostAsync(IdentityProviderConfiguration.Domain + "/oauth2/token", BuildContent(parameters)));
            var content = ExecuteSync(response.Content.ReadAsStringAsync());
            if ((int)response.StatusCode < 400)
            {
                Token = JsonConvert.DeserializeObject<OauthToken>(content);
                if (Token != null)
                {
                    AuthotizationState = AuthotizationStates.Authorized;
                    AddHeader(client, Token);
                    RunRenew(client, CancellationTokenSorce.Token);
                }

            }
            else
            {
                throw new ApplicationException(content);
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
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token?.AccessToken);
        }
        public virtual async Task<object> GetUserProfile(HttpClient httpClient)
        {

            var response = await _httpClient.GetAsync(IdentityProviderConfiguration.Domain + "/oauth2/user_profile");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject(content);
            }
            else
            {
                throw new ApplicationException(content);
            }

        }
    }
}
