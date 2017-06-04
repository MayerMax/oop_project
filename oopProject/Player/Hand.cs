using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Hand
    {
        private List<FootballCard> hand;
        public int HandSize { get { return hand.Count; }}
        public bool Any { get { return hand.Any(); } }

        public Hand(List<FootballCard> starterPack) {
            hand = starterPack;
        }
        
        public bool Remove(FootballCard card) {
            if (hand.Contains(card)) {
                hand.Remove(card);
                return true;
            }
            return false;
        }

        public void InsertCard(FootballCard card) {
            hand.Add(card);
        }

        public FootballCard this[int index] => hand[index];

        public void SortCardsByCriteria(string attributeCriteria) {
            hand.Sort();
            // implement Icomparer ? 
        }

        public IEnumerable<FootballCard> GetCardsByRank(int count) {
            if (count > HandSize)
                count = HandSize -1;
            return hand.OrderByDescending(card => card.Rank).Take(count);
        }

        public bool Contains(FootballCard card) => hand.Contains(card);
    }
}
