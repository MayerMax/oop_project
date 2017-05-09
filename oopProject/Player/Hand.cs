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
        public int HandSize { get { return hand.Count; } private set { } }

        public Hand(List<FootballCard> starterPack) {
            hand = starterPack;
            HandSize = hand.Count;   
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
