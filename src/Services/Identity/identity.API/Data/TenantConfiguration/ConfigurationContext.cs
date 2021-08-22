using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

using identity.API.Entities;


namespace identity.API.Data.TenantConfiguration
{
    public class ConfigurationContext : IConfigurationContext
    {
        public ConfigurationContext(IConfiguration configuration)
        {
            MongoClient client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            IMongoDatabase database = client.GetDatabase("TenantRegistration");

            Tenant = database.GetCollection<Tenant>("Tenants");

            Sites = database.GetCollection<Sites>("Sites");
        }

        public IMongoCollection<Sites> Sites { get; set; }
        public IMongoCollection<Tenant> Tenant { get; set; }
    }
}
