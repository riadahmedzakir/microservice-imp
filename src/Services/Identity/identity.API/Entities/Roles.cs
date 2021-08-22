using MongoDB.Bson.Serialization.Attributes;

namespace identity.API.Entities
{
    public class Roles
    {
        [BsonId]
        public string ItemId { get; set; }
        public string RoleName { get; set; }
        public bool IsDisabled { get; set; }

    }
}
