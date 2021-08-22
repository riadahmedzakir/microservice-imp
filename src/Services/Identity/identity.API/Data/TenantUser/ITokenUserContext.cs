using MongoDB.Driver;

using identity.API.Entities;

namespace identity.API.Data.TenantUser
{
    public interface ITokenUserContext
    {
        IMongoCollection<User> Users { get; set; }
    }
}
