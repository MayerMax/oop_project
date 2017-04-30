using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject.Game
{
    abstract class Card<T> where T: PlayerInfo
    {
        public readonly string cardName;

        protected readonly T playerInfo;
        protected readonly int maxAttributeRank;

        
        public Card(T pl, int maxValue) {
            playerInfo = pl;
            cardName = pl["Name"];
            maxAttributeRank = maxValue;
        }


        public abstract int EvaluateGeneralRank();
        public abstract int EvaluateAttackRank();
        public abstract int EvaluateDefendRank();
        public abstract int EvaluateBalanceRank();

        public abstract IEnumerable<string> GeneralDescription();

        protected abstract int ClampByModulo(int curValue);


    }
}
