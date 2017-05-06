using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;

namespace oopProject
{
    public class MongoDatabase : IDatabase
    {
        private IMongoCollection<BsonDocument> collection;

        public MongoDatabase()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Players");
            collection = database.GetCollection<BsonDocument>("playerAttributes");
        }

        public PlayerInfo GetPlayerInfo(string name)
        {
            var document = collection.AsQueryable()
                                     .Where(attrs => attrs["Name"].Equals(name))
                                     .First();
            document.Remove("_id");
            var attributes = document.ToDictionary(elem => elem.Name, elem => elem.Value.AsString);
            return new FootballPlayerInfo(attributes);
        }

        public IEnumerable<PlayerInfo> WithEqualAttribute(string attribute)
        {
            var a = collection.Aggregate()
                              .Group(new BsonDocument { { "_id", "$Club" } })
                              .ToList();
            var d = collection.Find(doc => doc[attribute]);
            var b = new Dictionary<string, string>();
            return new List<PlayerInfo>() { new FootballPlayerInfo(b) };
        }
    }
}
