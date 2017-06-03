using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Deck
    {
        public static readonly int DECK_SIZE = 50;

        private Stack<FootballCard> deck;

        public Deck(FootballDatabase db) {
            deck = new Stack<FootballCard>( db.GetCards(DECK_SIZE));
        }

        public FootballCard GetCard() {
            return deck.Pop();
        }

        public bool Any { get { return deck.Any(); } }

    }
}
