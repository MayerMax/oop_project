using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject.Game
{
    class FootballCard : Card<FootballPlayerInfo>
    {
        public static readonly int MAXCHAR = 10;
        private Func<List<string>, int> evaluateFunction;

        public int Rank { get; private set; }
        public readonly int Defend;
        public readonly int Attack;
        public readonly int Midfield;

        public FootballCard(FootballPlayerInfo playerInfo) : base(playerInfo, MAXCHAR) {
            evaluateFunction = AverageThroughValues;
            Rank = EvaluateGeneralRank();
            Defend = EvaluateDefendRank();
            Midfield = EvaluateBalanceRank();
            Attack = EvaluateAttackRank();         
        }
        // probably generalize idea of evaluation functions by creating for each single class, where intrinsic logic will be added.
        protected override int EvaluateAttackRank() => ClampByModulo(evaluateFunction(playerInfo.ATT));


        protected override int EvaluateBalanceRank() => ClampByModulo(evaluateFunction(playerInfo.MID));


        protected override int EvaluateDefendRank() => ClampByModulo(evaluateFunction(playerInfo.DEF));


        protected override int EvaluateGeneralRank() => playerInfo.ParseAttribute("Rating");
        

        public override IEnumerable<string> GeneralDescription() => playerInfo.GENERAL.Select(f => f);
        

        protected override int ClampByModulo(int curValue)
        {
            return curValue / MAXCHAR;
        }

        
        private int AverageThroughValues(List<string> list) {
            return (int) list.Select(el => playerInfo.ParseAttribute(el)).Average();
        }

        
    }
}
