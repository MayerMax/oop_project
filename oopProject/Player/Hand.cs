using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Hand
    {
        public static int MaxHandSize;
        private List<FootballCard> hand;
        private Squad squad;
        public int HandSize { get; private set; }

        public Hand(List<FootballCard> starterPack, Squad playerFormation) {
            if (starterPack.Count > MaxHandSize)
                throw new InvalidOperationException("Too many players");
            hand = starterPack;
            HandSize = hand.Count;
            squad = playerFormation;
        }

        public bool RemoveToSquad(FootballCard card) {
            return false;
        }

        public bool Remove(FootballCard card) {
            if (hand.Contains(card)) {
                hand.Remove(card);
                HandSize = hand.Count;
                return true;
            }
            return false;
        }

        public bool Swap(FootballCard newCard, FootballCard oldCard) {
            if (!hand.Contains(oldCard))
                throw new InvalidOperationException("There is no such card that you want to swap");
            var oldCardIndex = hand.IndexOf(oldCard);
            hand[oldCardIndex] = newCard;
            return true;
        }
    }
}
