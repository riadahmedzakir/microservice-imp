using MongoDB.Bson.Serialization.Attributes;

namespace identity.API.Entities
{
    public class User
    {
        [BsonId]
        public string ItemId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string[] Roles { get; set; }
    }
}
