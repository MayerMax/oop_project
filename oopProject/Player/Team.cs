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
        
        public Team(Squad starts, Hand subs) {
            Squad = starts;
            Hand = subs;
        }

        public void SubstitutionFromHandToSquad(ZoneType type, int oldCardPosition, 
                                                FootballCard newCard, int newCardPosition) {
            Hand.Remove(newCard);
            var oldCard = Squad.Remove(type, oldCardPosition);
            Squad.Insert(type, newCard, newCardPosition);
            Hand.InsertCard(oldCard);
        }

        
    }
}
