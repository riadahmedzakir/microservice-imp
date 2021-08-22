using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Entities
{
    public class Tenant
    {
        [BsonId]
        public string ItemId { get; set; }
        public string HostName { get; set; }
        public string TenantName { get; set; }
        public string TenantId { get; set; }
        public string ClientSecret { get; set; }
        public string[] AllowedScopes { get; set; }
    }
}
