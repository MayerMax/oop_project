using oopProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Squad
    {
        public static int SQUAD_SIZE = 11;
        public static int TOTAL_FULL_AMOUNT = 4;
        public static int TOTAL_SHORT_AMOUNT = 3;

        public static int ZONE_LIMIT = 5;
        private Dictionary<ZoneType, Zone> squad;
        
        
        public string SquadName { get; private set; }

        public Squad(string name, FootballCard keeper, Dictionary<ZoneType, List<FootballCard>> team) {
            SquadName = name;
            squad.Add(ZoneType.GK, new Zone(ZoneType.GK, team[ZoneType.GK]));
            squad.Add(ZoneType.DEF, new Zone(ZoneType.DEF, team[ZoneType.DEF]));
            squad.Add(ZoneType.MID, new Zone(ZoneType.MID, team[ZoneType.MID]));
            squad.Add(ZoneType.ATT, new Zone(ZoneType.ATT, team[ZoneType.ATT]));
        }

        public void Remove(ZoneType type,FootballCard oldCard) {
            var zone = squad[type];
            zone.RemoveCard(oldCard);  
        }

        public void Insert(ZoneType type, FootballCard card) {
            var zone = squad[type];
            zone.InsertCard(card);
        }

        // Validates that formation is correct, expected "4-5-1"
        public static bool ValidateSquad(string formation) {
            int[] eachzoneSize = formation.Split('-').Select(elem => int.Parse(elem)).ToArray();
            if (eachzoneSize.Length != TOTAL_SHORT_AMOUNT)
                return false;

            int totalSum = eachzoneSize.Sum();
            if (totalSum != SQUAD_SIZE - 1)
                return false;

            foreach (var count in eachzoneSize)
                if (count > ZONE_LIMIT)
                    return false;
            return true;
        }
           
    }
}
