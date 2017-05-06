using oopProject.Game;
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

        public static int ZONE_LIMIT = 4;
        private Dictionary<ZoneType, Zone> squad;
        
        public string SquadName { get; private set; }
        public Squad(string name, string givenForamtion) {
            SquadName = name;
            squad = FillSquadBeforeStart(givenForamtion);
        }

        private Dictionary<ZoneType, Zone> FillSquadBeforeStart(string givenFormation) {
            int[] eachzoneSize = givenFormation.Split('-').Select(elem => int.Parse(elem)).ToArray();
            if (eachzoneSize.Length != TOTAL_FULL_AMOUNT || eachzoneSize.Length != TOTAL_SHORT_AMOUNT)
                throw new ArgumentException("Not a football squad");
            int totalSum = eachzoneSize.Sum();
            if (totalSum != SQUAD_SIZE || totalSum != SQUAD_SIZE -1)
                throw new ArgumentException("Not a football squad");

            foreach (var count in eachzoneSize)
                if (!(count < ZONE_LIMIT))
                    throw new ArgumentException("Incorect amount of players in zone");

            // here we need only to write next code
            //squad.Add(ZoneType.GK, DB.GetCardOfType()); to fill goalkeepeer
            //squad.Add(ZoneType.DEF, DB.GetFewCards(eachzoneSize[(int) ZoneType.DEF]));
            //.. and so on

            throw new NotImplementedException(); 
        }


    }
}
