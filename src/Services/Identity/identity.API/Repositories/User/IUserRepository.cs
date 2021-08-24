using FluentValidation.Results;
using identity.API.Entities;
using Microsoft.AspNetCore.Http;
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
        Task<User> GetUserByEmail(string email);
        Task<ValidationResult> CrateUser(User user, string TenantId);
    }
}
