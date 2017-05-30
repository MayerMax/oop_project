﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace oopProject
{
    [TestFixture]
    class BasicTests
    {
        private FootballDatabase db = new FootballDatabase(new MongoDatabase());
        
        [Test]
        public void InitPlayer() {
            var player = new Player(db, "Max", "4-3-3");
            Console.WriteLine(player.PrintTeam());
        }
        [Test]
        public void CheckDeck() {
            var deck = new Deck(db);
            var card = deck.GetCard();
            Assert.True(card.GetType() == typeof(FootballCard));
        }


        [Test]
        public void SquadValidation() {
            Assert.True(Squad.ValidateSquad("4-3-3"));
            Assert.True(Squad.ValidateSquad("3-5-2"));
            Assert.False(Squad.ValidateSquad("5-1-1-1"));
        }

        [Test]
        public void CheckActionReflection() {
            var player = new Player(db, "Max", "4-3-3");
            var holder = new ActionHolder(new List<Type> { Type.GetType("oopProject.SwapAction") });

            holder.SetToPlayer(player);
            var res = holder.Get();
            Assert.Equals(1, res.Count());

        }

    }
}