﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class Position
    {
        public FootballCard Card { get; set; }
        public readonly int Number;
        public bool IsFree { get { return Card == null; } }

        public Position(int place, FootballCard card) {
            Card = card;
            Number = place;
        }

        public override bool Equals(object obj)
        {
            var card = (FootballCard)obj;
            return card.CardName.Equals(Card.CardName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void Release() {
            Card.CurrentZone = ZoneType.NONE;
            Card = null;
        }
    }
}
