using MongoDB.Bson.Serialization.Attributes;

namespace identity.API.Entities
{
    public class FeatureRoleMaps
    {
        [BsonId]
        public string ItemId { get; set; }
        public string FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string RoleName { get; set; }
    }
}
