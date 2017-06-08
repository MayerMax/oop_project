using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Linq;

namespace oopProject
{
    public class MongoDatabase : IDatabase
    {
        private readonly IMongoCollection<BsonDocument> collection;

        public MongoDatabase()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Players");
            collection = database.GetCollection<BsonDocument>("playerAttributes");
        }

        public PlayerInfo GetPlayerInfo(string name)
        {
            var document = collection
                                     .AsQueryable()
                                     .First(attrs => attrs["Name"].Equals(name));
            var attributes = BsonDocToDictionary(document);
            return new FootballPlayerInfo(attributes);
        }

        public PlayerInfo GetPlayerOfType(params string[] types)
        {
            var bsonTypes = types.Select(type => (BsonValue)type);
            var player = collection.AsQueryable()
                                    .Where(attrs => bsonTypes.Contains(attrs["Club_Position"]))
                                    .Sample(1)
                                    .First();
            var attributes = BsonDocToDictionary(player);
            return new FootballPlayerInfo(attributes);
        }

        public IEnumerable<PlayerInfo> GetPlayers(int count)
        {
            foreach (var document in collection.AsQueryable().Sample(count))
                yield return new FootballPlayerInfo(BsonDocToDictionary(document));
        }

        private Dictionary<string, string> BsonDocToDictionary(BsonDocument document)
        {
            document.Remove("_id");
            return document.ToDictionary(elem => elem.Name, elem => elem.Value.AsString);
        }
    }
}
