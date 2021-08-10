using identity.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Data
{
    public class UserContextSeed        
    {
        // The general idea of a Seed Method is to initialize data into a database that is being created by Code
        // First or evolved by Migrations. This data is often test data, but may also be reference data such as
        // ists of known Students, Courses, etc. When the data is initialized, it does the following −
        // # Checks whether or not the target database already exists.
        // # If it does, then the current Code First model is compared with the model stored in metadata in the database.
        // # The database is dropped if the current model does not match the model in the database.
        // # The database is created if it was dropped or didn’t exist in the first place.
        // # If the database was created, then the initializer Seed method is called.
        public static void SeedData(IMongoCollection<User> userCollection)
        {
            bool existUser = userCollection.Find(p => true).Any();
            if (!existUser) { 
            }
        }
    }
}
