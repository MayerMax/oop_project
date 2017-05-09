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

        public void SubstitutionFromHandToSquad(ZoneType type, FootballCard oldCard, FootballCard newCard) {
            Hand.Remove(newCard);
            Squad.Remove(type, oldCard);
            Squad.Insert(type, newCard);
            Hand.InsertCard(oldCard);
        }

    }
}
