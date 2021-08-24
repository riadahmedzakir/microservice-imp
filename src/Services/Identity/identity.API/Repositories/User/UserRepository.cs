using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;

using identity.API.Data;
using identity.API.Entities;

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

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Find(p => p.Email == email).FirstOrDefaultAsync();
        }

        public async Task CrateUser(User user)
        {

        }
    }
}
