using oopProject.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Line
    {
        private List<Position> cards;
        private ZoneType type;
        public bool HasBall { get; private set; }
        private int size;

        public Line(List<FootballCard> cards, ZoneType type, bool ballInZone)
        {
            size = cards.Count();
            this.cards = FillWithCards(cards);
            this.type = type;
            HasBall = ballInZone;

        }

        private List<Position> FillWithCards(List<FootballCard> cards) {
            var places = new List<Position>(cards.Count);
            for (int i = 0; i < cards.Count; i++)
                places[i] = new Position(i, cards[i]);
            return places;
        }

        public void Remove(FootballCard oldCard) {
            var tempPlace = new Position(0, oldCard);
            var index = cards.IndexOf(tempPlace);
            cards[index].ReleasePosition();
        }

        public void Insert(FootballCard card) {
            foreach (var pos in cards)
                if (pos.IsFree)
                    pos.Card = card;
            throw new InvalidOperationException("No free space available");
        }

        public int ZonePower() {
            var totalAvgDef = cards.Select(f => f.Card.Defend).Average();
            var totalAvgMid = cards.Select(f => f.Card.Midfield).Average();
            var totalAvgAtt = cards.Select(f => f.Card.Attack).Average();
            var averageRank = cards.Select(f => f.Card.Rank).Average();
            return (int) ( averageRank * Math.Min(totalAvgDef, Math.Min(totalAvgAtt, totalAvgMid)));
        }


    }
}
