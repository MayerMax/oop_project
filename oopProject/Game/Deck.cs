﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace oopProject
{
    public class Deck
    {
        public static readonly int DECK_SIZE = 50;

        private Stack<FootballCard> deck;

        public Deck(IFootballDatabase db) {
            deck = new Stack<FootballCard>( db.GetCards(DECK_SIZE));
        }

        public FootballCard GetCard() {
            return deck.Pop();
        }

        public bool Any => deck.Any();
    }
}
