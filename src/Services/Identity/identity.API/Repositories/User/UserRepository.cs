using identity.API.Data;
using identity.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<User>> GetUser()
        {
            return await _context.Users.Find(p => true).ToListAsync();
        }

        public async Task<User> GetUser(string ItemId)
        {
            return await _context.Users.Find(p => p.ItemId == ItemId).FirstOrDefaultAsync();
        }
    }
}
