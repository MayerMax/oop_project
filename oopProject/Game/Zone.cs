using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace oopProject
{
    public class Zone : BaseZone
    {
        private List<Position> cards;

        public bool Any => Count > 0;
        public int Count => cards.Count(f => !(f.IsFree));

        public Zone(ZoneType type, List<FootballCard> cards) : base(cards.Count, type)
        {
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
            cards[cardIndex].Release();
            return card;
        }

        public void RemoveDeadCards() {
            foreach (var position in cards)
                if (position.Card.Rank <= 0)
                    position.Release();
        }
        public void InsertCard(FootballCard card, int position) {
            if (!cards[position].IsFree)
                throw new InvalidOperationException("This position is not available");
            card.MoveToZone(Type);
            cards[position].Card = card;
        }

        public string Print() {
            var names =
                cards.Where(pos => !pos.IsFree)
                .Select(pos => $"{pos.Number + 1}.{pos.Card.CardName}({pos.Card.Rank}) ");
            return string.Join("--", names);

        }

        private List<Position> FillWithCards(List<FootballCard> cards)
        {
            var places = new List<Position>(cards.Count+1);
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].MoveToZone(Type);
                places.Insert(i, new Position(i, cards[i]));
            }
            return places;
        }
    }

    static class ZoneExtensions
    {
        private static Random rand = new Random();

        public static double PassPower(this Zone zone)
        {
            var totalAvgDef = zone.CardsAttributes(f => f.Defend).Average();
            var totalAvgMid = zone.CardsAttributes(f => f.Midfield).Average();
            var totalAvgAtt = zone.CardsAttributes(f => f.Attack).Average();
            return (totalAvgDef + totalAvgMid + totalAvgAtt) / 3;
        }

        public static double DefendPower(this Zone zone)
            => zone.CardsAttributes(f => f.Defend).Average();

        public static double WithAdditionalPower(this Zone zone, FootballCard additional) {
            var defendPower = zone.DefendPower();
            return defendPower + 0.1 * additional.Rank;
        }

        public static double InterceptPower(this Zone zone)
        {
            var def = zone.CardsAttributes(f => f.Defend).Average() * 0.8;
            var mid = zone.CardsAttributes(f => f.Midfield).Average() * 0.6;
            return (def + mid) / 2;
        }

        public static double ShootPower(this Zone zone)
        {
            var att = zone.CardsAttributes(f => f.Attack).Average();
            var rank = zone.CardsAttributes(f => f.Rank).Average();
            return Math.Min(att, rank);
        }

        public static double PressurePower(this Zone zone) =>
            zone.CardsAttributes(f => f.Rank).Average() * rand.NextDouble();
        

        public static void DecreaseRandomCardRank(this Zone zone, int percent) {
            var randomPosition = zone[rand.Next(0, zone.Count)];
            if (!randomPosition.IsFree) {
                var card = randomPosition.Card;
                card.DecreaseRank(percent);
            }
        }

        private static IEnumerable<double> CardsAttributes(this Zone zone, Func<FootballCard, double> attributeSelector)
        {
            var attributes = zone.GetCards().Select(attributeSelector);
            if (!attributes.Any())
                return new List<double>() { 0 };
            return attributes;
        }
    }
}
