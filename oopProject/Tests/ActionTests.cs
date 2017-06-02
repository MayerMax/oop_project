using System;
using NUnit.Framework;
using System.Linq;

namespace oopProject
{
    [TestFixture]
    class ActionTests
    {
        private FootballDatabase db = new FootballDatabase(new MongoDatabase());

        [Test]
        public void PassAction()
        {
            var ball = new Ball();
            var first = new Player("Max", Squad.GetRandomSquad(db, "N", "3-4-3"),
                                   new Hand(db.GetCards(10).ToList()), ball);
            var second = new Player("Leo", Squad.GetRandomSquad(db, "M", "3-2-5"),
                                    new Hand(db.GetCards(10).ToList()), ball);
            var action = new PassAction(first.Team);
            Assert.True(first.Team.HasBall);
            Assert.AreEqual(first.Team.Ball.BallPlace, ZoneType.MID);
            var parameters = new PassParameters(second.Team);
            if (action.Execute(parameters))
                Assert.AreEqual(first.Team.Ball.BallPlace, ZoneType.ATT);
            else
                Assert.AreEqual(first.Team.Ball.BallPlace, ZoneType.MID);
                // should be checked that another team has the ball now
        }
    }
}
