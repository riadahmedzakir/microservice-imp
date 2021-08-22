using identity.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Data.TenantConfiguration
{
    public interface IConfigurationContext
    {
        IMongoCollection<Sites> Sites { get; set; }
        IMongoCollection<Tenant> Tenant { get; set; }
    }
}
