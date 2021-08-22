using identity.API.Data.TenantConfiguration;
using identity.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Repositories.TenantIdentity
{
    public class TenantIdentityRepository : ITenantIdentityRepository
    {
        private readonly IConfigurationContext _context;
        public TenantIdentityRepository(IConfigurationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Tenant> GetIdentity(string Referer, string Origin)
        {
            return await _context.Tenant.Find(p => p.HostName == Referer && p.HostName == Origin).FirstOrDefaultAsync();
        }
    }
}
