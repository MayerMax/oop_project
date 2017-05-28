using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class SwapParameters : IParameters
    {
        public readonly int OldCardPosition;
        public readonly int NewCardPosition;
        public readonly FootballCard NewCard;
        public readonly ZoneType OldCardZone;

        public SwapParameters(int oldCardPosition, ZoneType oldCardZone,
                              FootballCard newCard, int newCardPosition)
        {
            OldCardPosition = oldCardPosition;
            NewCardPosition = newCardPosition;
            NewCard = newCard;
            OldCardZone = oldCardZone;
        }
    }
}
