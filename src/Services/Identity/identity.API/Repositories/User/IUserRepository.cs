using identity.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUser();
        Task<User> GetUser(string ItemId);
    }
}
