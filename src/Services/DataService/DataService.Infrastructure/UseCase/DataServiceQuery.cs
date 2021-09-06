using DataService.Application.Interface;
using Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Application.UseCase
{
    public class DataServiceQuery<T> : IQueryModel<T>
    {
        private IMongoDatabase _database;
        public IMongoCollection<T> collection;
        public DataServiceQuery(string connectionString, string tenantId)
        {
            MongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(tenantId);
        }

        public Task<IReadOnlyList<T>> GetCollection(string collectionName, string queryString, string fields, int pageNumber, int pageSize)
        {
            //Type CollectionType = Type.GetType("Entities." + collectionName);
            //Activator.CreateInstance(CollectionType);

            collection = _database.GetCollection<T>(collectionName);

            FilterDefinition<T> filter = queryString;

            IReadOnlyList<T> data = (IReadOnlyList<T>)collection
                .Find(filter)
                .Limit(pageSize)
                .Skip(pageSize * pageNumber)
                .Project(fields)
                .ToList();

            return (Task<IReadOnlyList<T>>)data;
        }
    }
}
