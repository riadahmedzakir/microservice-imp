using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;

using identity.API.Data;
using identity.API.Entities;
using Microsoft.AspNetCore.Http;
using identity.API.Validators;
using FluentValidation.Results;
using identity.API.Repositories.IdentityService;

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

        public async Task<ValidationResult> CrateUser(User user, string TenantId)
        {
            HasValidUserInformation validator = new HasValidUserInformation(TenantId);
            ValidationResult result = validator.Validate(user);

            if (result.IsValid)
            {
                IUserContext _userContext = new UserContext(TenantId);
                string userPassword = user.Password;
                PasswordHash hash = new PasswordHash(userPassword);
                byte[] hashBytes = hash.ToArray();
                user.Password = "";
                user.PasswordHash = hashBytes;

                await _userContext.Users.InsertOneAsync(user);
            }

            return result;
        }
    }
}
