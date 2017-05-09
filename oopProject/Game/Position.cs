using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject.Game
{
    class Position
    {
        public FootballCard Card { get; set; }
        public int Number { get; private set; }
        public bool IsFree { get { return Card == null; } }
        public Position(int place, FootballCard card) {
            Card = card;
            Number = place;
        }

        public override bool Equals(object obj)
        {
            var card = (FootballCard)obj;
            if (card.cardName.Equals(Card.cardName))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void ReleasePosition() {
            Card = null;
        }
    }
}
