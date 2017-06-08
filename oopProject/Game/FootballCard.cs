using System;
using System.Collections.Generic;
using System.Linq;

namespace oopProject
{
    public class FootballCard : Card<FootballPlayerInfo>
    {
        public static readonly int MAXCHAR = 10;
        private Func<List<string>, int> evaluateFunction;

        public double Rank { get; private set; }

        public int Defend { get; set; }
        public int Attack { get; set; }
        public int Midfield { get; set; }

        public readonly ZoneType PreferredZone;
        public ZoneType CurrentZone { get; set; }

        public override string ToString() {
            var list = GeneralDescription().ToList();
            return PlayerInfo["Name"] + " " + string.Join(" ", list.Select(value => value + " " + PlayerInfo[value])) + " " + PreferredZone.ToString();
            
        }

        public FootballCard(FootballPlayerInfo playerInfo, ZoneType curZone) : base(playerInfo, MAXCHAR) {
            evaluateFunction = AverageThroughValues;
            Defend = EvaluateDefendRank();
            Midfield = EvaluateBalanceRank();
            Attack = EvaluateAttackRank();
            CurrentZone = ZoneType.NONE;
            PreferredZone = curZone;
            Rank = EvaluateGeneralRank();
        }

        // probably generalize idea of evaluation functions by creating for each single class, where intrinsic logic will be added.
        protected override int EvaluateAttackRank() => ClampByModulo(evaluateFunction(PlayerInfo.ATT));


        protected override int EvaluateBalanceRank() => ClampByModulo(evaluateFunction(PlayerInfo.MID));


        protected override int EvaluateDefendRank() => ClampByModulo(evaluateFunction(PlayerInfo.DEF));


        protected override double EvaluateGeneralRank() {

            var optimum = PlayerInfo.ParseAttribute("Rating") / MAXCHAR;
            if (CurrentZone != PreferredZone)
            {
                optimum = (optimum / 3);
                if (optimum == 0)
                    return 1;
            }
            return optimum;
        }

        public void DecreaseRank(int percent) {
            double coef = 1 - percent / 100;
            Rank = (int) (Rank * coef);
        }
        

        public override IEnumerable<string> GeneralDescription() => PlayerInfo.GENERAL.Select(f => f);
        

        protected override int ClampByModulo(int curValue)
        {
            return curValue / MAXCHAR;
        }

        
        private int AverageThroughValues(List<string> list) {
            return (int) list.Select(el => PlayerInfo.ParseAttribute(el)).Average();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is FootballCard))
                return false;
            var card = (FootballCard)obj;
            return CardName == card.CardName;
        }

        public override int GetHashCode()
        {
            return CardName.GetHashCode();
        }
    }
}
