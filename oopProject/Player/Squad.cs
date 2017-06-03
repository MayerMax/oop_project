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

        public readonly string Formation;
        public string SquadName { get; private set; }

        public bool Any => squad.Values.Select(z => z.Any).Aggregate((a, b) => a | b);
        public bool AnyZone(ZoneType zone) => squad[zone].Any;

        public Squad(string name, FootballCard keeper, 
                     Dictionary<ZoneType, List<FootballCard>> team, string formation) {
            SquadName = name;
            Formation = formation;
            squad = new Dictionary<ZoneType, Zone>
            {
                { ZoneType.GK, new Zone(ZoneType.GK, new List<FootballCard> { keeper }) },
                { ZoneType.DEF, new Zone(ZoneType.DEF, team[ZoneType.DEF]) },
                { ZoneType.MID, new Zone(ZoneType.MID, team[ZoneType.MID]) },
                { ZoneType.ATT, new Zone(ZoneType.ATT, team[ZoneType.ATT]) }
            };
        }

        public FootballCard Remove(ZoneType type, int cardIndex) {
            var zone = squad[type];
            return zone.RemoveCard(cardIndex);  
        }

        public void Insert(ZoneType type, FootballCard card, int position) {
            var zone = squad[type];
            zone.InsertCard(card, position);
        }

        public double GetZonePower(ZoneType zoneType, Func<Zone, double> calculate) => calculate(squad[zoneType]);
        
        public string Print() {
            StringBuilder sb = new StringBuilder();
            foreach (var zone in squad)
                sb.Append($"{(int)zone.Key}. {zone.Key} - {zone.Value.Print()}\n");
            return sb.ToString();
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
           
        public static Squad GetRandomSquad(FootballDatabase db, string name, string formation) =>
            new Squad(name, db.GetCardOfType(ZoneType.GK), GetSquadZones(db, formation), formation);

        private static Dictionary<ZoneType, List<FootballCard>> GetSquadZones(FootballDatabase db, 
                                                                              string formation)
        {
            int[] eachzoneSize = formation.Split('-').Select(elem => int.Parse(elem)).ToArray();
            var squad = new Dictionary<ZoneType, List<FootballCard>>
            {
                { ZoneType.DEF, db.GetCards(eachzoneSize[0]).ToList() },
                { ZoneType.MID, db.GetCards(eachzoneSize[1]).ToList() },
                { ZoneType.ATT, db.GetCards(eachzoneSize[2]).ToList() }
            };
            return squad;
        }
    }
}
