using FluentValidation;
using identity.API.Data;
using identity.API.Data.Role;
using identity.API.Entities;
using identity.API.Repositories.Appsettings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Validators
{
    public class HasValidUserInformation : AbstractValidator<User>
    {
        public HasValidUserInformation(string TenantId)
        {
            RuleFor(x => x.ItemId).NotEmpty().WithMessage("User ItemId is required");
            RuleFor(x => x.ItemId).MustAsync((x, y) => IsItemIdAvailable(x, TenantId)).WithMessage("Duplicate ItemId");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Email).MustAsync((x, y) => IsEmailAvailable(x, TenantId)).WithMessage("Duplicate email address");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Roles).NotEmpty().WithMessage("Role is required");
            RuleFor(x => x.Roles).MustAsync((x, y) => IsValidRole(x, TenantId)).WithMessage("Roles are invalid or disabled");
        }

        public async Task<bool> IsItemIdAvailable(string itemId, string TenantId)
        {
            IUserContext _userContext = new UserContext(TenantId);

            var userfilter = Builders<User>.Filter.Eq(item => item.ItemId, itemId);
            List<User> users = await _userContext.Users
                .Find(userfilter)
                .ToListAsync();
            if (users.Count == 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IsEmailAvailable(string email, string TenantId)
        {
            IUserContext _userContext = new UserContext(TenantId);

            var userfilter = Builders<User>.Filter.Eq(item => item.Email, email);
            List<User> users = await _userContext.Users
                .Find(userfilter)
                .ToListAsync();
            if (users.Count == 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IsValidRole(string[] roleList, string TenantId)
        {
            IRoleContext _roleContext = new RoleContext(TenantId); ;

            var rolesfilter = Builders<Roles>.Filter.In(item => item.RoleName, roleList);
            List<Roles> roles = await _roleContext.Roles
                .Find(rolesfilter)
                .ToListAsync();
            if (roles.Count == 0)
            {
                return false;
            }
            else
            {
                if (roles.Count == roleList.Length)
                {
                    foreach (Roles role in roles)
                    {
                        if (role.IsDisabled)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }
    }
}
