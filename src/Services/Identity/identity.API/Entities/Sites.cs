using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Entities
{
    public class Sites
    {
        [BsonId]
        public string ItemId { get; set; }
        public string TenantId { get; set; }
        public string HostName { get; set; }
    }
}
