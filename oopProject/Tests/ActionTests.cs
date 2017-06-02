using System;
using NUnit.Framework;

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
            var first = new Player(db, "Max", "3-4-3", ball);
            var second = new Player(db, "Leo", "3-2-5", ball);
            var action = new PassAction(first.Team);
            Assert.True(first.Team.HasBall);
            Assert.AreEqual(first.Team.Ball.BallPlace, ZoneType.MID);
            var parameters = new PassParameters(second.Team);
            var a = first.Team.Squad.GetZonePower(ZoneType.MID);
            var b = second.Team.Squad.GetZonePower(ZoneType.MID);
            if (action.Execute(parameters))
                Assert.AreEqual(first.Team.Ball.BallPlace, ZoneType.ATT);
            else
                Assert.AreEqual(first.Team.Ball.BallPlace, ZoneType.MID);
                
        }
    }
}
