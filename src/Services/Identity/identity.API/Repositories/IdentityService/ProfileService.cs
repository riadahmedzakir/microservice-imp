using identity.API.Data.TenantUser;
using identity.API.Repositories.IdentityService;
using IdentityServer4.Models;
using IdentityServer4.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace identity.API.Repositories.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly ITokenUserContext _tokenUserContext;

        public ProfileService(ITokenUserContext tokenUserContext)
        {
            _tokenUserContext = tokenUserContext ?? throw new ArgumentNullException(nameof(tokenUserContext));
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                if (!string.IsNullOrEmpty(userId?.Value))
                {
                    var user = await _tokenUserContext.Users.Find(p => p.ItemId == userId.Value).FirstOrDefaultAsync();

                    if (user != null)
                    {
                        List<Claim> claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                        context.IssuedClaims = claims;
                    }
                }
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
