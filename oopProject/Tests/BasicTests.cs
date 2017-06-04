using System;
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
            var player = new Player("Max", Squad.GetRandomSquad(db, "N", "4-3-3"),
                                    new Hand(db.GetCards(10).ToList()), new Ball());
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
        public void CheckActionReflection()
        {
            var player = new Player("Max", Squad.GetRandomSquad(db, "N", "4-3-3"),
                                    new Hand(db.GetCards(10).ToList()), new Ball());
            var a = new List<Action>() { new SwapAction(player.Team) };
            var swapParameters = new SwapParameters(1, ZoneType.ATT, null);

            var holder = new ActionHolder(new List<Type> { Type.GetType("oopProject.SwapAction"),
                                                           Type.GetType("oopProject.PassAction"),
                                                           Type.GetType("oopProject.BonusAction")});
            holder.SetToPlayer(player);
            var res = holder.Get();
            Assert.AreEqual(3, res.Count());
        }
    }
}
