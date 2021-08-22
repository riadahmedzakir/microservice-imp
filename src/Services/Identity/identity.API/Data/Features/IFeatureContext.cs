using MongoDB.Driver;

using identity.API.Entities;

namespace identity.API.Data.Feature
{
    public interface IFeatureContext
    {
        IMongoCollection<FeatureEndpointMaps> FeatureEndpointMaps { get; set; }
        IMongoCollection<FeatureRoleMaps> FeatureRoleMaps { get; set; }
    }
}
