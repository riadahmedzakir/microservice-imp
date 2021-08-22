using identity.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Repositories.TenantIdentity
{
    public interface ITenantIdentityRepository
    {
        Task<Tenant> GetIdentity(string Referer, string Origin);
    }
}
