using identity.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Data
{
    public class UserContext : IUserContext
    {
        public UserContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Users = database.GetCollection<User>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //UserContextSeed.SeedData(Users);
        }
        public IMongoCollection<User> Users { get; set; }
    }
}
