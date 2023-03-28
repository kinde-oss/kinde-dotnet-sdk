using Kinde.Api.Models.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Api.Models.User
{
    public class KindeSSOUser
    {
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
        public string Id { get { return GetClaim<string>("sub"); } }
        public string GivenName { get { return GetClaim<string>("given_name"); } }
        public string FamilyName { get { return GetClaim<string>("family_name");  } }
        public string Email { get { return GetClaim<string>("email"); } }

        public string? GetOrganisation()
        {
            return GetClaim<string>("org_code");
        }
        public string[]? GetOrganisations()
        {
            return GetClaim<string[]>("org_codes");
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
            if (permissions!=null && permissions.Any(x => x == key))
            {
                return new OrganisationPermission() { Id = key, OrganisationId = GetOrganisation(), Granted = true };
            }
            return new OrganisationPermission() { Id = key, OrganisationId = GetOrganisation(), Granted = false };
        }
        public object? GetClaim(string key)
        {
            return GetClaim<object>(key);
        }
        protected T? GetClaim<T>(string key)
        {
            var jObject = default(object?);
            if (AccessToken?.Payload != null && AccessToken.Payload.TryGetValue(key, out var accessTokenVal))
            {
                jObject = accessTokenVal;
            }
            if (IdToken?.Payload != null && IdToken.Payload.TryGetValue(key, out var idTokenVal))
            {
                jObject = idTokenVal;
            }
            if (jObject != null)
            {
                // As JWT JObjects are internal, there is a cheat via deserialisation of value
                if (typeof(T).IsArray) return JArray.Parse(jObject.ToString()).ToObject<T>();
                if (typeof(T).IsByRef) return JObject.Parse(jObject.ToString()).ToObject<T>();
                return (T)jObject;
            }
            return default(T);
        
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
    }
}
