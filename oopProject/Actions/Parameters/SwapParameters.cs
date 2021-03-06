﻿namespace oopProject
{
    public class SwapParameters : IParameters
    {
        public readonly int CardPosition;
        public readonly ZoneType OldCardZone;
        public readonly int NewCardPosition;

        public SwapParameters(int cardPosition, ZoneType oldCardZone,
                              int newCardPosition)
        {
            CardPosition = cardPosition;
            NewCardPosition = newCardPosition;
            OldCardZone = oldCardZone;
        }
    }
}
