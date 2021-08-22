using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

using identity.API.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System;

namespace identity.API.Data.TenantUser
{
    public class TokenUserContext : ITokenUserContext
    {
        private string TenantId;
        public TokenUserContext(IConfiguration configuration, IHttpContextAccessor _httpContextAccessor)
        {
            if (_httpContextAccessor.HttpContext.Request.HasFormContentType)
            {
                TenantId = _httpContextAccessor.HttpContext.Request.Form["client_id"];
                MongoClient client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                IMongoDatabase database = client.GetDatabase(TenantId);
                Users = database.GetCollection<User>("Users");
            }
        }
        public IMongoCollection<User> Users { get; set; }
    }
}
