using oopProject.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class FootballCard : Card<FootballPlayerInfo>
    {
        public static readonly int MAXCHAR = 10;
        private Func<List<string>, int> evaluateFunction;

        public int Rank { get; private set; }
        public readonly int Defend;
        public readonly int Attack;
        public readonly int Midfield;

        public readonly ZoneType PreferredZone;
        public ZoneType CurrentZone { get; private set; }

        public FootballCard(FootballPlayerInfo playerInfo, ZoneType curZone) : base(playerInfo, MAXCHAR) {
            evaluateFunction = AverageThroughValues;
            Rank = EvaluateGeneralRank();
            Defend = EvaluateDefendRank();
            Midfield = EvaluateBalanceRank();
            Attack = EvaluateAttackRank();
            CurrentZone = curZone;
            PreferredZone = GetCardPrefferedZone(); /// TODO    
        }

        // probably generalize idea of evaluation functions by creating for each single class, where intrinsic logic will be added.
        protected override int EvaluateAttackRank() => ClampByModulo(evaluateFunction(playerInfo.ATT));


        protected override int EvaluateBalanceRank() => ClampByModulo(evaluateFunction(playerInfo.MID));


        protected override int EvaluateDefendRank() => ClampByModulo(evaluateFunction(playerInfo.DEF));


        protected override int EvaluateGeneralRank() {

            var optimum = playerInfo.ParseAttribute("Rating");
            if (CurrentZone != PreferredZone)
            {
                optimum = (optimum / 3);
                if (optimum == 0)
                    return 1;

            }
            return optimum;
        }
        

        public override IEnumerable<string> GeneralDescription() => playerInfo.GENERAL.Select(f => f);
        

        protected override int ClampByModulo(int curValue)
        {
            return curValue / MAXCHAR;
        }

        
        private int AverageThroughValues(List<string> list) {
            return (int) list.Select(el => playerInfo.ParseAttribute(el)).Average();
        }

        private ZoneType GetCardPrefferedZone() {
            var positionDescription = playerInfo["Position"];
            /// here we need to put next logic : how to get ZoneType base on String description of player position
            /// This is where Util folder might be useful - put code with method in this folder
            throw new NotImplementedException("Not Implemented");
        }
    }
}
