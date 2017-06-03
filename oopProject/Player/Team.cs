using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Team
    {
        public readonly Squad Squad;
        public readonly Hand Hand;
        public Ball Ball { get; private set; }

        public bool HasBall { get { return Ball != null && Ball.IsOwner(this); } }

        public Team(Squad starts, Hand subs, Ball ball) {
            Squad = starts;
            Hand = subs;
            Ball = ball;
            Ball.AddObserver(this);
        }

        public void Update(Ball ball) => Ball = ball;

        public void SubstitutionFromHandToSquad(ZoneType type, int oldCardPosition, 
                                                FootballCard newCard, int newCardPosition) {
            Hand.Remove(newCard);
            var oldCard = Squad.Remove(type, oldCardPosition);
            Squad.Insert(type, newCard, newCardPosition);
            Hand.InsertCard(oldCard);
        }
    }
}
