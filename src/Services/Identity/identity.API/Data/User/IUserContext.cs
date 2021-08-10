using identity.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Data
{
    public interface IUserContext
    {
        IMongoCollection<User> Users { get; set; }
    }
}
