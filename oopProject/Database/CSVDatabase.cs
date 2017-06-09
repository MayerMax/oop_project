using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.IO;

namespace oopProject
{
    public class CSVDatabase :IDatabase
    {
        private const string SourcePath = "FullData.csv";
        private const int PlayersAmount = 300;

        private Dictionary<string, Dictionary<string, string>> database;

        public CSVDatabase()
        {
            database = new Dictionary<string, Dictionary<string, string>>();
            FillDatabase();
        }

        public PlayerInfo GetPlayerInfo(string name)
            => new FootballPlayerInfo(database[name]);

        public PlayerInfo GetPlayerOfType(params string[] types)
            => new FootballPlayerInfo(database.Values.Where(attrs => types.Contains(attrs["Club_Position"]))
                                                     .OrderBy(n => Guid.NewGuid())
                                                     .First());

        public IEnumerable<PlayerInfo> GetPlayers(int count)
            => database.Values.OrderBy(n => Guid.NewGuid())
                              .Take(count)
                              .Select(attrs => new FootballPlayerInfo(attrs));

        private void FillDatabase()
        {
            var count = 0;
            try
            {
                using (var sr = new StreamReader(SourcePath))
                {
                    var parser = new CsvParser(sr);
                    var header = parser.Read().ToList();
                    while (true)
                    {
                        var info = parser.Read();
                        if (info == null || count == PlayersAmount)
                            break;

                        var attributes = header.Zip(info, (k, v) => new {Key = k, Value = v})
                            .ToDictionary(x => x.Key, x => x.Value);
                        database[attributes["Name"]] = attributes;
                        count++;
                    }
                }
            }
            catch (Exception) { throw new DatabaseException("Cannot initialize database");}
        }
    }
}
