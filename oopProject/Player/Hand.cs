using System.Collections.Generic;
using System.Linq;

namespace oopProject
{
    public class Hand
    {
        private List<FootballCard> hand;
        public int HandSize => hand.Count;
        public bool Any => hand.Any();
        public FootballCard Peek => Any ? hand[hand.Count - 1] : null;

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

        public bool Contains(int index) => index < hand.Count;

        public string Print()
        {
            var cards = string.Join(", ", hand.Select(c => $"{c.CardName}({c.MaxRankWithDamage})").ToArray());
            return $"[{cards}]";
        }
    }
}
