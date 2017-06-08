using System.Collections.Generic;

namespace oopProject
{
    public abstract class Card<T>
        where T: PlayerInfo
    {
        public readonly string CardName;

        protected readonly T PlayerInfo;
        protected readonly int MaxAttributeRank;


        protected Card(T pl, int maxValue) {
            PlayerInfo = pl;
            CardName = pl["Name"];
            MaxAttributeRank = maxValue;
        }


        protected abstract double EvaluateGeneralRank();
        protected abstract int EvaluateAttackRank();
        protected abstract int EvaluateDefendRank();
        protected abstract int EvaluateBalanceRank();

        public abstract IEnumerable<string> GeneralDescription();

        protected abstract int ClampByModulo(int curValue);
    }
}
