using Duende.IdentityServer.Models;

namespace IdentityServer.DbContext
{
    public static class Configuration
    {

        public static IEnumerable<IdentityResource> GetIdentityResources =>
              new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiScope> GetApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(name:"read",displayName:"Read Some of the data"),
                new ApiScope(name:"create",displayName:"Create Some of the data"),
                new ApiScope(name:"update",displayName:"Update Some of the data"),
                new ApiScope(name:"delete",displayName:"Delete Some of the data"),
                new ApiScope(name:"full",displayName:"Full Access to data"),
            };

        public static IEnumerable<Client> GetClients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "global_administrator",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {"full"},
                    //RedirectUris = { "https://localhost:5000/signin-oidc" },
                    //PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
                },
                new Client
                {
                    ClientId = "methodist_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {"read","create","update","delete"}
                },
                new Client
                {
                    ClientId = "admin_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {"read","create","update","delete"}
                },
                new Client
                {
                    ClientId = "moder_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {"read","create","update","delete"}
                },
                new Client
                {
                    ClientId = "teacher_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {"read","create","update","delete"}
                },
                new Client
                {
                    ClientId = "student_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {"read","update"}
                },

            };









    }
}
