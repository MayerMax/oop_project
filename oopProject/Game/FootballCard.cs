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
        public double TotalDamage { get; private set; }
        public double MaxRankWithDamage => MaxRank - TotalDamage;

        private double MaxRank => PlayerInfo.ParseAttribute("Rating") / MAXCHAR;

        public readonly ZoneType PreferredZone;
        public ZoneType CurrentZone { get; private set; }

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
            Rank = MaxRank;
        }

        // probably generalize idea of evaluation functions by creating for each single class, where intrinsic logic will be added.
        protected override int EvaluateAttackRank() => ClampByModulo(evaluateFunction(PlayerInfo.ATT));


        protected override int EvaluateBalanceRank() => ClampByModulo(evaluateFunction(PlayerInfo.MID));


        protected override int EvaluateDefendRank() => ClampByModulo(evaluateFunction(PlayerInfo.DEF));


        protected override double EvaluateGeneralRank()
        {
            var rank = MaxRank;
            if (CurrentZone != PreferredZone)
            {
                rank = Math.Round(rank / 3, 1);
                if (rank == 0)
                    return 1;
            }
            return rank;
        }

        public void DecreaseRank(double percent) {
            var coef = 1 - percent / 100;
            var decreased = Math.Round(Rank * coef, 1);
            TotalDamage += Rank - decreased;
            Rank = decreased;
        }

        public void MoveToZone(ZoneType zone)
        {
            CurrentZone = zone;
            Rank = EvaluateGeneralRank() - TotalDamage;
        }

        public void LeaveZone() => CurrentZone = ZoneType.NONE;

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
