using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Zone : BaseZone, IGameElement
    {
        public bool HasBall { get; private set; } // TODO
        private List<Position> cards;
        public int ZonePower { get { return CalculateZonePower(); } }


        public Zone(ZoneType type, List<FootballCard> cards, bool hasBall = false) : base(cards.Count, type)
        {
            HasBall = hasBall;
            this.cards = FillWithCards(cards);
        }

        public Position this[int index]
        {
            get
            {
                if (index < 0 || index >= cards.Count)
                    throw new IndexOutOfRangeException();
                return cards[index];
            }
        }

        public IEnumerable<FootballCard> GetCards()
        {
            foreach (var card in cards.Select(p => p.Card).Where(c => c != null))
                yield return card;
        }

        public FootballCard RemoveCard(int cardIndex) {
            var card = cards[cardIndex].Card;
            cards[cardIndex].ReleasePosition();
            return card;
        }

        public void InsertCard(FootballCard card, int position) {
            if (!cards[position].IsFree)
                throw new InvalidOperationException("This position is not available");
            cards[position].Card = card;
        }

        private int CalculateZonePower()
        {
            var totalAvgDef = cards.Select(f => f.Card.Defend).Average();
            var totalAvgMid = cards.Select(f => f.Card.Midfield).Average();
            var totalAvgAtt = cards.Select(f => f.Card.Attack).Average();
            var averageRank = cards.Select(f => f.Card.Rank).Average();
            return (int)(averageRank * Math.Min(totalAvgDef, Math.Min(totalAvgAtt, totalAvgMid)));
        }
        public string Print() {
            var names = 
                cards.Where(pos => !pos.IsFree)
                .Select(pos =>$"{pos.Number +1}.{pos.Card.cardName}");

            return string.Join("--", names);

        }

        private List<Position> FillWithCards(List<FootballCard> cards)
        {

            var places = new List<Position>(cards.Count+1);
            for (int i = 0; i < cards.Count; i++)
               places.Insert(i, new Position(i, cards[i]));
            return places;
        }
    }
}
