using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace oopProject
{
    [TestFixture]
    class BallActionsTesting
    {
        private FootballDatabase db = new FootballDatabase(new MongoDatabase());
       
        private Tuple<Player, Player> GetPlayers(Ball ball) {
            var first = new Player(db, "Max", "3-4-3", ball);
            var second = new Player(db, "Leo", "3-2-5", ball);
            return Tuple.Create(first, second);
        }

        [Test]
        public void CheckMovement() {
            Ball ball = new Ball();
            var players = GetPlayers(ball);
            ball.Move();
            Assert.True(ball.BallPlace == ZoneType.ATT);
            Assert.AreEqual(players.Item1, ball.Owner);
        }

        [Test]
        public void CheckInterception() {
            Ball ball = new Ball();
            var players = GetPlayers(ball);
            ball.Move();
            ball.Intercept(players.Item2);
            Assert.True(ball.BallPlace == ZoneType.DEF);
            Assert.AreEqual(players.Item2, ball.Owner);
        }
    }
}
