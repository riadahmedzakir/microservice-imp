using System;
using System.Threading.Tasks;

using IdentityServer4.Models;
using IdentityServer4.Stores;

using MongoDB.Driver;
using identity.API.Data.TenantConfiguration;
using identity.API.Entities;
using IdentityServer4;

namespace identity.API.Repositories.IdentityService
{
    public class ClientStore : IClientStore
    {
        private readonly IConfigurationContext _context;
        public ClientStore(IConfigurationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            Tenant tenant = await _context.Tenant.Find(p => p.TenantId == clientId).FirstOrDefaultAsync();

            if (tenant != null)
            {
                Client client = new Client
                {
                    ClientId = tenant.TenantId,
                    ClientSecrets = new[] { new Secret(tenant.ClientSecret.Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = { "http://" + tenant.HostName + ":8080" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                };

                foreach (string scope in tenant.AllowedScopes)
                {
                    client.AllowedScopes.Add(scope);
                }
                return await Task.FromResult(client);
            }

            return null;
        }
    }
}
