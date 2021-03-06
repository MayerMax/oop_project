﻿namespace oopProject
{
    public class Team
    {
        public readonly Squad Squad;
        public readonly Hand Hand;
        public IBall Ball { get; private set; }

        public bool HasBall => Ball != null && Ball.IsOwner(this);

        public Team(Squad starts, Hand subs, IBall ball) {
            Squad = starts;
            Hand = subs;
            Ball = ball;
            Ball.AddObserver(this);
        }

        public void Update(IBall ball) => Ball = ball;

        public void SubstitutionFromHandToSquad(ZoneType type, int cardPosition, 
                                                int newCardPosition) {
            var substitution = Hand[newCardPosition];
            Hand.Remove(substitution);
            var oldCard = Squad.Remove(type, cardPosition);
            Squad.Insert(type, substitution, cardPosition);
            Hand.InsertCard(oldCard);
        }

        public void SwapInSquad(ZoneType first, int firstPos, 
                                ZoneType second, int secondPos)
            => Squad.Swap(first, firstPos, second, secondPos);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Team))
                return false;
            var team = (Team)obj;
            return Squad.Name == team.Squad.Name;
        }
    }
}
