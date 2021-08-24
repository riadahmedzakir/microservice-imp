using identity.API.Entities;
using identity.API.Repositories.Appsettings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace identity.API.Data.Role
{
    public class RoleContext : IRoleContext
    {
        public RoleContext(string TenantId)
        {
            MongoClient client = new MongoClient(AppsettingsRepository.AppSetting("ConnectionString"));
            IMongoDatabase database = client.GetDatabase(TenantId);

            Roles = database.GetCollection<Roles>("Roles");
        }

        public RoleContext(IConfiguration configuration, IHttpContextAccessor _httpContextAccessor)
        {
            List<Claim> UserClaims = _httpContextAccessor.HttpContext.User.Claims.ToList();
            string TenantId = UserClaims?.FirstOrDefault(x => x.Type.Equals("client_id", StringComparison.OrdinalIgnoreCase))?.Value;

            MongoClient client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            IMongoDatabase database = client.GetDatabase(TenantId);

            Roles = database.GetCollection<Roles>("Roles");
        }

        public IMongoCollection<Roles> Roles { get; set; }
    }
}
