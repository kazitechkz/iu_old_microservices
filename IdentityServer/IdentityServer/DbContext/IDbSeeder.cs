namespace IdentityServer.DbContext
{
    public interface IDbSeeder
    {
        public Task SeedAsync();

    }
}