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
        public int HandSize { get { return hand.Count; } private set { } }

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

        public void InsertCard(FootballCard card) {
            hand.Add(card);

        }

        public void SortCardsByCriteria(string attributeCriteria) {
            hand.Sort();
            // implement Icomparer ? 
        }

        public IEnumerable<FootballCard> GetCardsByRank(int count) {
            if (count > HandSize)
                count = HandSize -1;
            return hand.OrderByDescending(card => card.Rank).Take(count);
        }
    }
}
