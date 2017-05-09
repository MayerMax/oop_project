using oopProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class FootballDatabase
    {
        private IDatabase database;
        private Dictionary<ZoneType, string[]> types = new Dictionary<ZoneType, string[]>
        {
            [ZoneType.GK] = new[] { "GK" },
            //TODO
        };

        public FootballDatabase(IDatabase database)
        {
            this.database = database;
        }

        public FootballCard GetCardOfType(ZoneType zone)
        {
            return new FootballCard((FootballPlayerInfo)database.GetPlayerOfType(types[zone]), ZoneType.NONE);
        }

        public IEnumerable<FootballCard> GetCards(int count)
        {
            foreach (var playerInfo in database.GetPlayers(count))
                yield return new FootballCard((FootballPlayerInfo)playerInfo, ZoneType.NONE);
        }
    }
}
