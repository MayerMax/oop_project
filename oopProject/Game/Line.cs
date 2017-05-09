﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Line
    {
        private List<FootballCard> cards;
        private ZoneType type;
        public bool HasBall { get; private set; }
        private int size;

        public Line(List<FootballCard> cards, ZoneType type, bool ballInZone)
        {
            size = cards.Count();
            this.cards = cards;
            HasBall = ballInZone;

        }

        public void ReplaceCard(FootballCard oldCard, FootballCard newCard ) {
            int index = cards.IndexOf(oldCard);
            cards[index] = newCard;
        }

        public void TrashCard(FootballCard card) {
            cards.Remove(card);
            size--;
        }
        public int ZonePower() {
            var totalAvgDef = cards.Select(f => f.Defend).Average();
            var totalAvgMid = cards.Select(f => f.Midfield).Average();
            var totalAvgAtt = cards.Select(f => f.Attack).Average();
            var averageRank = cards.Select(f => f.Rank).Average();
            return (int) ( averageRank * Math.Min(totalAvgDef, Math.Min(totalAvgAtt, totalAvgMid)));
        }


    }
}
