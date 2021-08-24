using MongoDB.Driver;

using identity.API.Entities;

namespace identity.API.Data.Role
{
    public interface IRoleContext
    {
        IMongoCollection<Roles> Roles { get; set; }
    }
}
