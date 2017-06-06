using oopProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class FootballCard : Card<FootballPlayerInfo>
    {
        public static readonly int MAXCHAR = 10;
        private Func<List<string>, int> evaluateFunction;

        private double rank;
        public double Rank { get { return EvaluateGeneralRank(); }
            private set {
                rank = value;
            }
        }

        public int Defend { get; set; }
        public int Attack { get; set; }
        public int Midfield { get; set; }

        public readonly ZoneType PreferredZone;
        public ZoneType CurrentZone { get; set; }

        public override string ToString() {
            var list = GeneralDescription().ToList();
            return playerInfo["Name"] + " " + string.Join(" ", list.Select(value => value + " " + playerInfo[value])) + " " + PreferredZone.ToString();
            
        }

        public FootballCard(FootballPlayerInfo playerInfo, ZoneType curZone) : base(playerInfo, MAXCHAR) {
            evaluateFunction = AverageThroughValues;
            Defend = EvaluateDefendRank();
            Midfield = EvaluateBalanceRank();
            Attack = EvaluateAttackRank();
            CurrentZone = curZone;
            PreferredZone = curZone;
            Rank = EvaluateGeneralRank();
        }

        // probably generalize idea of evaluation functions by creating for each single class, where intrinsic logic will be added.
        protected override int EvaluateAttackRank() => ClampByModulo(evaluateFunction(playerInfo.ATT));


        protected override int EvaluateBalanceRank() => ClampByModulo(evaluateFunction(playerInfo.MID));


        protected override int EvaluateDefendRank() => ClampByModulo(evaluateFunction(playerInfo.DEF));


        protected override double EvaluateGeneralRank() {

            var optimum = playerInfo.ParseAttribute("Rating") / MAXCHAR;
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
        

        public override IEnumerable<string> GeneralDescription() => playerInfo.GENERAL.Select(f => f);
        

        protected override int ClampByModulo(int curValue)
        {
            return curValue / MAXCHAR;
        }

        
        private int AverageThroughValues(List<string> list) {
            return (int) list.Select(el => playerInfo.ParseAttribute(el)).Average();
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
