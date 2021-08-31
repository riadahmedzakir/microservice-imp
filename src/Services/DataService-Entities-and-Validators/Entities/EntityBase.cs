using System;

using MongoDB.Bson.Serialization.Attributes;


namespace Entities
{
    public class EntityBase : IRowLevelSecurity
    {
        [BsonId]
        public string ItemId { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string Language { get; set; }
        public virtual DateTime LastUpdateDate { get; set; }
        public virtual string LastUpdatedBy { get; set; }
        public string[] Tags { get; set; }
        public string[] RolesAllowedToRead { get; set; }
        public string[] IdsAllowedToRead { get; set; }
        public string[] RolesAllowedToWrite { get; set; }
        public string[] IdsAllowedToWrite { get; set; }
        public string[] RolesAllowedToUpdate { get; set; }
        public string[] IdsAllowedToUpdate { get; set; }
        public string[] RolesAllowedToDelete { get; set; }
        public string[] IdsAllowedToDelete { get; set; }
    }
}
