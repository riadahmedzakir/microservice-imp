using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

using identity.API.Entities;

namespace identity.API.Data.Feature
{
    public class FeaturesContext : IFeatureContext
    {
        public FeaturesContext(IConfiguration configuration, IHttpContextAccessor _httpContextAccessor)
        {
            List<Claim> UserClaims = _httpContextAccessor.HttpContext.User.Claims.ToList();
            string TenantId = UserClaims?.FirstOrDefault(x => x.Type.Equals("client_id", StringComparison.OrdinalIgnoreCase))?.Value;

            MongoClient client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            IMongoDatabase database = client.GetDatabase(TenantId);

            FeatureEndpointMaps = database.GetCollection<FeatureEndpointMaps>("FeatureEndpointMaps");
            FeatureRoleMaps = database.GetCollection<FeatureRoleMaps>("FeatureRoleMaps");
        }

        public IMongoCollection<FeatureEndpointMaps> FeatureEndpointMaps { get; set; }
        public IMongoCollection<FeatureRoleMaps> FeatureRoleMaps { get; set; }
    }
}
