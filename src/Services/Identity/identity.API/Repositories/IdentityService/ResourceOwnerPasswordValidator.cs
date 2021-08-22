using identity.API.Data.TenantUser;
using identity.API.Entities;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace identity.API.Repositories.IdentityService
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ITokenUserContext _tokenUserContext;

        public ResourceOwnerPasswordValidator(ITokenUserContext tokenUserContext)
        {
            _tokenUserContext = tokenUserContext ?? throw new ArgumentNullException(nameof(tokenUserContext));
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await _tokenUserContext.Users.Find(p => p.Email == context.UserName).FirstOrDefaultAsync();
                if (user != null)
                {
                    // check if password match - remember to hash password if stored as hash in db
                    if (user.Password == context.Password)
                    {
                        context.Result = new GrantValidationResult(
                            subject: user.ItemId,
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user));

                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        public static List<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserId", user.ItemId ?? ""),
                new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                new Claim("FirstName", user.FirstName ?? ""),
                new Claim("LastName", user.LastName ?? ""),
                new Claim("PhoneNumber", user.PhoneNumber ?? "")
        };

            foreach (string role in user.Roles)
            {
                //claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim("Roles", role));
            }

            return claims;
        }
    }
}