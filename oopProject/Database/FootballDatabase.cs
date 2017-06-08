using System.Collections.Generic;
using System.Linq;

namespace oopProject
{
    public class FootballDatabase : IFootballDatabase
    {
        private readonly IDatabase database;
        private readonly Dictionary<ZoneType, string[]> types = new Dictionary<ZoneType, string[]>
        {
            [ZoneType.GK] = new[] { "GK" },
            [ZoneType.DEF] = new[] {"CB", "LCB", "RCB", "LB", "RB", "Sub" },
            [ZoneType.MID] = new[] {"CM", "LDM", "RDM", "CDM", "CAM", "LM", "RM" },
            [ZoneType.ATT] = new[] { "ST", "CF", "LW", "RW"}
        };
        private readonly Dictionary<string, ZoneType> reversedTypes;


        public FootballDatabase(IDatabase database)
        {
            this.database = database;
            reversedTypes = CompleteRotation();
        }

        public FootballCard GetCardOfType(ZoneType zone)
        {
            return new FootballCard((FootballPlayerInfo)database.GetPlayerOfType(types[zone]), zone);
        }

        public IEnumerable<FootballCard> GetCards(int count)
        {
            foreach (var playerInfo in database.GetPlayers(count))
            {
                var availableZones = playerInfo["Preffered_Position"];
                ZoneType mostPreferred;
                try
                {
                    mostPreferred = reversedTypes[availableZones.Split('/').ToList()[0]];
                }
                catch (KeyNotFoundException)
                {
                    mostPreferred = ZoneType.MID;
                }
                yield return new FootballCard((FootballPlayerInfo)playerInfo, mostPreferred);
            }
        }

        private Dictionary<string, ZoneType> CompleteRotation() {
            var reversedDictionary = new Dictionary<string, ZoneType>();
            foreach (var record in types) {
                foreach (var position in record.Value)
                    reversedDictionary[position] = record.Key;
            }
            return reversedDictionary;
        }
    }
}
