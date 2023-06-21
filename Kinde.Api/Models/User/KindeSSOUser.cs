using Kinde.Api.Models.Tokens;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace Kinde.Api.Models.User
{
    public class KindeSSOUser
    {
        private static readonly Dictionary<string, string> FlagType = new Dictionary<string, string>
        {
            { "s", "string" },
            { "i", "integer" },
            { "b", "boolean" },
        };

        protected OauthToken TokenSource { get; set; }

        public JwtSecurityToken AccessToken { get; set; }

        public JwtSecurityToken IdToken { get; set; }

        public static KindeSSOUser FromToken(OauthToken token)
        {
            var handler = new JwtSecurityTokenHandler();

            var user = new KindeSSOUser();
            if (!string.IsNullOrEmpty(token.AccessToken) && handler.CanReadToken(token.AccessToken))
            {
                user.AccessToken = handler.ReadJwtToken(token.AccessToken);
            }

            if (!string.IsNullOrEmpty(token.IdToken) && handler.CanReadToken(token.IdToken))
            {
                Debug.WriteLine("Id token is empty, so user profile will be unavaliable");
                user.IdToken = handler.ReadJwtToken(token.IdToken);
            }

            user.TokenSource = token;
            return user;
        }

        public KindeSSOUser()
        {
        }

        public bool IsAuthorized { get { return !TokenSource?.IsExpired ?? false; } }

        public string Id { get { return GetClaim<string>("sub", "id_token"); } }

        public string GivenName { get { return GetClaim<string>("given_name", "id_token"); } }

        public string FamilyName { get { return GetClaim<string>("family_name", "id_token"); } }

        public string Email { get { return GetClaim<string>("email", "id_token"); } }

        public string Picture { get { return GetClaim<string>("picture", "id_token"); } }

        public string? GetOrganisation()
        {
            return GetClaim<string>("org_code");
        }

        public string[]? GetOrganisations()
        {
            return GetClaim<string[]>("org_codes", "id_token");
        }

        public OrganisationPermissionsCollection GetPermissions()
        {
            return new OrganisationPermissionsCollection()
            {
                Permissions = GetClaim<string[]>("permissions")?.ToList() ?? new List<string>(),
                OrganisationId = GetOrganisation()
            };
        }

        public OrganisationPermission GetPermission(string key)
        {
            var permissions = GetClaim<string[]>("permissions");
            if (permissions != null && permissions.Any(x => x == key))
            {
                return new OrganisationPermission() { Id = key, OrganisationId = GetOrganisation(), Granted = true };
            }
            return new OrganisationPermission() { Id = key, OrganisationId = GetOrganisation(), Granted = false };
        }

        public KindeClaim? GetClaim(string key, string tokenType = "access_token")
        {
            var value = GetClaim<object>(key, tokenType);
            return value == null ? null : new KindeClaim
            {
                Name = key,
                Value = value
            };
        }

        protected T? GetClaim<T>(string key, string tokenType = "access_token")
        {
            if (!new[] { "access_token", "id_token" }.Contains(tokenType))
            {
                throw new ApplicationException("Invalid token type");
            }

            var jObject = default(object?);
            if (tokenType == "access_token" && AccessToken?.Payload != null && AccessToken.Payload.TryGetValue(key, out var accessTokenVal))
            {
                jObject = accessTokenVal;
            }

            if (tokenType == "id_token" && IdToken?.Payload != null && IdToken.Payload.TryGetValue(key, out var idTokenVal))
            {
                jObject = idTokenVal;
            }

            if (jObject != null)
            {
                // As JWT JObjects are internal, there is a cheat via deserialisation of value
                if (typeof(T).IsArray) return JArray.Parse(jObject.ToString()).ToObject<T>();
                if (typeof(T).IsByRef || jObject.GetType().Name == "JObject") return JObject.Parse(jObject.ToString()).ToObject<T>();
                return (T)jObject;
            }
            return default;
        }

        public FeatureFlag GetFlag(string code, FeatureFlagValue? defaultValue = null, string? flagType = null)
        {
            if (!string.IsNullOrEmpty(flagType) && !FlagType.ContainsKey(flagType))
            {
                throw new ApplicationException("Invalid flag type");
            }

            var flags = JObject.Parse(GetClaim("feature_flags")?.Value?.ToString() ?? "{}");
            if (!flags.ContainsKey(code) && defaultValue?.DefaultValue == null)
            {
                throw new ApplicationException("This flag was not found, and no default value has been provided");
            }

            var type = flags[code]?["t"]?.ToString();
            if (flags.ContainsKey(code) && !string.IsNullOrEmpty(flagType) && type != flagType)
            {
                throw new ApplicationException($"Flag \"{code}\" is type {FlagType[type]} - requested type {FlagType[flagType]}");
            }

            var flag = flags[code];
            var result = new FeatureFlag
            {
                Code = code,
                Value = flag?["v"]?.ToObject<object?>() ?? defaultValue?.DefaultValue,
                Is_default = !flags.ContainsKey(code),
                Type = flags.ContainsKey(code) ? FlagType[type] : null
            };
            return result;
        }

        public bool GetBooleanFlag(string code, bool? defaultValue = null) => Convert.ToBoolean(GetFlag(code, new FeatureFlagValue { DefaultValue = defaultValue }, "b").Value);

        public string GetStringFlag(string code, string? defaultValue = null) => Convert.ToString(GetFlag(code, new FeatureFlagValue { DefaultValue = defaultValue }, "s").Value);

        public int GetIntegerFlag(string code, int? defaultValue = null) => Convert.ToInt32(GetFlag(code, new FeatureFlagValue { DefaultValue = defaultValue }, "i").Value);
    }

    public class OrganisationPermission
    {
        public string Id { get; set; }
        public bool Granted { get; set; }
        public string? OrganisationId { get; set; }
    }

    public class OrganisationPermissionsCollection
    {
        public string OrganisationId { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class KindeClaim
    {
        public string? Name { get; set; }
        public object? Value { get; set; }
    }

    public class FeatureFlag
    {
        public string? Code { get; set; }
        public string? Type { get; set; }
        public object? Value { get; set; }
        public bool? Is_default { get; set; }
    }

    public class FeatureFlagValue
    {
        public object? DefaultValue { get; set; }
    }

    public class KindeUserDetail
    {
        public string Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
    }
}
