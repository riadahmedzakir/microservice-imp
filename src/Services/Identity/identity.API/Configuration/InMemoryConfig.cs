using System.Collections.Generic;
using System.Security.Claims;

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace identity.API.Configuration
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource> {
          new IdentityResources.OpenId(),
          new IdentityResources.Profile()
         };

        public static List<TestUser> GetUsers() => new List<TestUser> {
          new TestUser
          {
              SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
              Username = "Mick",
              Password = "MickPassword",
              Claims = new List<Claim>
              {
                  new Claim("given_name", "Mick"),
                  new Claim("family_name", "Mining"),
                  new Claim("langId", "en-US"),
              }
          },
          new TestUser
          {
              SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
              Username = "Jane",
              Password = "JanePassword",
              Claims = new List<Claim>
              {
                  new Claim("given_name", "Jane"),
                  new Claim("family_name", "Downing"),
                  new Claim("langId", "fr-FR"),
              }
          }
        };

        public static IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope>
        {
            new ApiScope("IsAllowedToAccessMircoservice", "Access to API")
        };

        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
        {
            new ApiResource("IsAllowedToAccessMircoservice", "Access to API")
            {
                Scopes = { "IsAllowedToAccessMircoservice" }
            }
        };

        public static IEnumerable<Client> GetClients() => new List<Client> {
           new Client
           {
                ClientId = "company-employee",
                ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowOfflineAccess = true,
                AllowedScopes = {
                   IdentityServerConstants.StandardScopes.OpenId,
                   IdentityServerConstants.StandardScopes.Profile,
                   "Api"
               }
            }
        };
    }
}
