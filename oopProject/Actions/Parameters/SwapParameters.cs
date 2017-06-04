using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class SwapParameters : IParameters
    {
        public readonly int CardPosition;
        public readonly FootballCard NewCard;
        public readonly ZoneType OldCardZone;

        public SwapParameters(int cardPosition, ZoneType oldCardZone,
                              FootballCard newCard)
        {
            CardPosition = cardPosition;
            NewCard = newCard;
            OldCardZone = oldCardZone;
        }
    }
}
