using MongoDB.Bson.Serialization.Attributes;

namespace identity.API.Entities
{
    public class FeatureEndpointMaps
    {
        [BsonId]
        public string ItemId { get; set; }
        public string FeatureId { get; set; }
        public string Service { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
