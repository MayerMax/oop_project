using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class SwapParameters : IParameters
    {
        public readonly int CardPosition;
        public readonly int NewCardPosition;
        public readonly ZoneType OldCardZone;

        public SwapParameters(int cardPosition, ZoneType oldCardZone,
                              int newCardPosition)
        {
            CardPosition = cardPosition;
            NewCardPosition = newCardPosition;
            OldCardZone = oldCardZone;
        }
    }
}
