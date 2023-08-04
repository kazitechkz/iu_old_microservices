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
                new ApiScope(name:"superadmin_scope",displayName:"SuperAdmin Scope"),
                new ApiScope(name:"methodist_scope",displayName:"Methodist Scope"),
                new ApiScope(name:"admin_scope",displayName:"Admin Scope"),
                new ApiScope(name:"moder_scope",displayName:"Moder Scope"),
                new ApiScope(name:"teacher_scope",displayName:"Teacher Scope"),
                new ApiScope(name:"student_scope",displayName:"Student Scope"),
            };

        public static IEnumerable<Client> GetClients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "global_administrator",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {"superadmin_scope"},
                    //RedirectUris = { "https://localhost:5000/signin-oidc" },
                    //PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
                },
                new Client
                {
                    ClientId = "methodist_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {"methodist_scope"}
                },
                new Client
                {
                    ClientId = "admin_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {"admin_scope"}
                },
                new Client
                {
                    ClientId = "moder_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "moder_scope" }
                },
                new Client
                {
                    ClientId = "teacher_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "teacher_scope" }
                },
                new Client
                {
                    ClientId = "student_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "student_scope" }
                },

            };









    }
}
