﻿using Duende.IdentityServer.Models;

namespace ids
{
    public static class Config
    {

        public static IEnumerable<IdentityResource> IdentityResources => new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("weatherapi.read"),
            new ApiScope("weatherapi.write"),
        };
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("weatherapi")
            {
                Scopes = new List<string> {"weatherapi.read", "weatherapi.write"},
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            }
        };

        public static IEnumerable<Client> Clients => new[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                AllowedScopes = {"weatherapi.read", "weatherapi.write"}
            },

            // interactive client using code flow + pkce v
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"https://localhost:7038/signin-oidc"},
                FrontChannelLogoutUri = "https://localhost:7038/signout-oidc",
                PostLogoutRedirectUris = {"https://localhost:7038/signout-callback-oidc"},

                AllowOfflineAccess = true,
                AllowedScopes = {"openid", "profile", "weatherapi.read"},
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            },
        };
    }
}
