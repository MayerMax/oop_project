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
            var player = new Player(db, "Max", "4-3-3", new Ball());
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
            var player = new Player(db, "Max", "4-3-3", new Ball());
            var a = new List<IAction>() { new SwapAction(player.Team) };
            var swapParameters = new SwapParameters(1, ZoneType.ATT, null, 3);

            var holder = new ActionHolder(new List<Type> { Type.GetType("oopProject.SwapAction"),
                                                           Type.GetType("oopProject.PassAction"),
                                                           Type.GetType("oopProject.BonusAction")});

            holder.SetToPlayer(player);
            var res = holder.Get();
            Assert.AreEqual(3, res.Count());

        }
    }
}
