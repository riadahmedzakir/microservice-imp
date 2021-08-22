﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

using identity.API.Entities;

namespace identity.API.Data
{
    public class UserContext : IUserContext
    {
        public UserContext(IConfiguration configuration, IHttpContextAccessor _httpContextAccessor)
        {
            List<Claim> UserClaims = _httpContextAccessor.HttpContext.User.Claims.ToList();
            string TenantId = UserClaims?.FirstOrDefault(x => x.Type.Equals("client_id", StringComparison.OrdinalIgnoreCase))?.Value;

            MongoClient client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            IMongoDatabase database = client.GetDatabase(TenantId);

            Users = database.GetCollection<User>("Users");
            //UserContextSeed.SeedData(Users);
        }
        public IMongoCollection<User> Users { get; set; }
    }
}
