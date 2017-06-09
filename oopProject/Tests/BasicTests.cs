using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace oopProject
{
    class BasicTests
    {
        private FootballDatabase db;
        private Game game;

        [SetUp]
        public void SetUp()
        {
            db = new FootballDatabase(new MongoDatabase());
            game = new Game(db, new Ball());
        }

        [Test]
        public void InitPlayer()
        {
            var player = new Player("Max", Squad.GetRandomSquad(db, "N", "4-3-3"),
                                    new Hand(db.GetCards(10).ToList()), new Ball());
            Console.WriteLine(player.PrintTeam(game.BallPlace));
        }
        [Test]
        public void CheckDeck()
        {
            var deck = new Deck(db);
            var card = deck.GetCard();
            Assert.True(card.GetType() == typeof(FootballCard));
        }


        [Test]
        public void SquadValidation()
        {
            Assert.True(Squad.ValidateSquad("4-3-3"));
            Assert.True(Squad.ValidateSquad("3-5-2"));
            Assert.False(Squad.ValidateSquad("5-1-1-1"));
        }
    }
}
