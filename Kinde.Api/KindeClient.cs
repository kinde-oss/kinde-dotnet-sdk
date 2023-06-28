using Kinde.Api.Enums;
using Kinde.Api.Flows;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.User;
using Kinde.Api.Models.Utils;

namespace Kinde.Api.Client
{
    public class KindeClient : ApiClient
    {
        #region Fields and Properties

        public static AuthorizationCodeStore<string, string> CodeStore = new AuthorizationCodeStore<string, string>();

        protected IAuthorizationFlow AuthorizationFlow { get; set; }

        /// <summary>
        /// Returns the profile for the current user	
        /// </summary>
        public KindeSSOUser User => AuthorizationFlow?.User;

        /// <summary>
        /// Returns the current authorization state of user
        /// </summary>
        public AuthorizationStates AuthorizationState => AuthorizationFlow?.AuthorizationState ?? AuthorizationStates.None;

        /// <summary>
        /// Returns the raw full Oauth token after logged from Kinde
        /// </summary>
        public OauthToken Token => AuthorizationFlow?.Token;

        /// <summary>
        /// To check user authenticated or not	
        /// </summary>
        public bool IsAuthenticated => Token != null && !Token.IsExpired;

        /// <summary>
        /// Returns the current identity provider configuration
        /// </summary>
        public IApplicationConfiguration IdentityProviderConfiguration { get; set; }

        #endregion

        /// <summary>
        /// Constructs kinde client instance to interact with kinde api and authorization flows
        /// </summary>
        /// <param name="identityProviderConfiguration">identity provider configuration.</param>
        /// <param name="httpClient">http client.</param>
        public KindeClient(IApplicationConfiguration identityProviderConfiguration, HttpClient httpClient) : base(httpClient, identityProviderConfiguration.Domain)
        {
            IdentityProviderConfiguration = identityProviderConfiguration;
            CodeStore = new AuthorizationCodeStore<string, string>();
        }

        #region Authorization flows

        protected override void AuthorizeRequest(HttpRequestMessage request, HttpClient httpClient)
        {
            AuthorizationFlow.AuthorizeRequest(request, httpClient);
        }

        protected async Task Authorize(IAuthorizationConfiguration authorizationConfiguration, bool register)
        {
            if (IdentityProviderConfiguration == null)
            {
                throw new ArgumentNullException("Identity provider configuration missing");
            }

            if (authorizationConfiguration == null)
            {
                throw new ArgumentNullException("Authorization flow configuration missing");
            }

            AuthorizationFlow = authorizationConfiguration.CreateAuthorizationFlow(IdentityProviderConfiguration);

            var state = await AuthorizationFlow.Authorize(HttpClient, register);
            if (state == AuthorizationStates.NonAuthorized)
            {
                throw new ApplicationException("Authorization failed");
            }
        }

        /// <summary>
        /// Constructs redirect url and sends user to Kinde to sign in	
        /// </summary>
        /// <param name="authorizationConfiguration">authorization configurations.</param>
        public async Task Authorize(IAuthorizationConfiguration authorizationConfiguration)
        {
            await Authorize(authorizationConfiguration, false);
        }

        /// <summary>
        /// Constructs redirect url and sends user to Kinde to sign up	
        /// </summary>
        /// <param name="authorizationConfiguration">authorization configurations.</param>
        public async Task Register(IAuthorizationConfiguration authorizationConfiguration)
        {
            await Authorize(authorizationConfiguration, true);
        }

        /// <summary>
        /// Constructs redirect url for Kinde user to sign in	
        /// </summary>
        /// <param name="state">The session id for instance to persist users authentication.</param>
        public async Task<string> GetRedirectionUrl(string state)
        {
            return await AuthorizationFlow.UserActionsResolver.GetLoginUrl(state);
        }

        /// <summary>
        /// Returns the raw Access token from URL after logged from Kinde		
        /// </summary>
        public async Task<string> GetToken()
        {
            return await AuthorizationFlow.GetOrRefreshToken(HttpClient);
        }

        /// <summary>
        /// Handle code received flows
        /// </summary>
        public static void OnCodeReceived(string code, string state)
        {
            CodeStore.Add(state, code);
            OnCodeConsumed(code, state);
        }

        /// <summary>
        /// Handle code consumed flows
        /// </summary>
        public static void OnCodeConsumed(string code, string state)
        {
            lock (CodeStore)
            {
                CodeStore.Remove(state);
            }
        }

        /// <summary>
        /// Logs the user out of Kinde
        /// </summary>
        public async Task<string> Logout()
        {
            await AuthorizationFlow.Logout(HttpClient);
            return IdentityProviderConfiguration.Domain + "/logout?redirect=" + IdentityProviderConfiguration.LogoutUrl;
        }

        /// <summary>
        /// Trying to get a new token using refresh_token	
        /// </summary>
        public async Task Renew()
        {
            await AuthorizationFlow.Renew(HttpClient);
        }

        #endregion

        #region User profile methods

        /// <summary>
        /// It returns user's information after successful authentication
        /// </summary>
        /// <remarks>
        /// Contains the id, given_name, family_name, email and picture of the currently logged in user. 
        /// </remarks>
        /// <returns>KindeUserDetail</returns>
        public KindeUserDetail GetUserDetails()
        {
            return new KindeUserDetail
            {
                Id = User.Id,
                Email = User.Email,
                FamilyName = User.FamilyName,
                GivenName = User.GivenName,
                Picture = User.Picture
            };
        }

        /// <summary>
        /// Get a claim from a token.
        /// </summary>
        /// <param name="key">The name of the claim.</param>
        /// <param name="tokenType">The token type to check: "access_token", "id_token"</param>
        /// <returns>KindeClaim</returns>
        public KindeClaim? GetClaim(string key, string tokenType = "access_token")
        {
            return User?.GetClaim(key, tokenType);
        }

        /// <summary>
        /// Returns all permissions for the current user for the organization they are logged into	
        /// </summary>
        /// <returns>OrganizationPermissionsCollection</returns>
        public OrganizationPermissionsCollection? GetPermissions()
        {
            return User?.GetPermissions();
        }

        /// <summary>
        /// Given a permission value, returns if it is granted or not (checks if permission key exists in the permissions claim array)
        /// And relevant org code (checking against claim org_code)
        /// </summary>
        /// <param name="key">The permission key.</param>
        /// <returns>OrganizationPermissionsCollection</returns>
        public OrganizationPermission? GetPermission(string key)
        {
            return User?.GetPermission(key);
        }

        /// <summary>
        /// Get details for the organization your user is logged into	
        /// </summary>
        /// <returns>The response is a orgCode</returns>
        public string? GetOrganization()
        {
            return User?.GetOrganization();
        }

        /// <summary>
        /// Gets an array of all organizations the user has access to	
        /// </summary>
        /// <returns>The response is all orgCodes</returns>
        public string[]? GetOrganizations()
        {
            return User?.GetOrganizations();
        }

        /// <summary>
        /// Get a flag from the feature_flags claim of the access_token.
        /// </summary>
        /// <param name="code">The name of the flag.</param>
        /// <param name="defaultValue">A fall back value if the flag isn't found.</param>
        /// <param name="flagType">The data type of the flag: "s", "b", "i"</param>
        /// <returns>FeatureFlag</returns>
        public FeatureFlag GetFlag(string code, FeatureFlagValue? defaultValue = null, string? flagType = null)
        {
            return User?.GetFlag(code, defaultValue, flagType);
        }

        /// <summary>
        /// Get a boolean flag from the feature_flags claim of the access_token.
        /// </summary>
        /// <param name="code">The name of the flag.</param>
        /// <param name="defaultValue">A fall back value if the flag isn't found.</param>
        /// <returns>flag as bool value</returns>
        public bool? GetBooleanFlag(string code, bool? defaultValue = null)
        {
            return User?.GetBooleanFlag(code, defaultValue);
        }

        /// <summary>
        /// Get a string flag from the feature_flags claim of the access_token.
        /// </summary>
        /// <param name="code">The name of the flag.</param>
        /// <param name="defaultValue">A fall back value if the flag isn't found.</param>
        /// <returns>flag as string value</returns>
        public string GetStringFlag(string code, string? defaultValue = null)
        {
            return User?.GetStringFlag(code, defaultValue);
        }

        /// <summary>
        /// Get a int flag from the feature_flags claim of the access_token.
        /// </summary>
        /// <param name="code">The name of the flag.</param>
        /// <param name="defaultValue">A fall back value if the flag isn't found.</param>
        /// <returns>flag as int value</returns>
        public int? GetIntegerFlag(string code, int? defaultValue = null)
        {
            return User?.GetIntegerFlag(code, defaultValue);
        }

        #endregion
    }
}