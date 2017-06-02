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
            var first = new Player("Max", Squad.GetRandomSquad(db, "N", "3-4-3"),
                                   new Hand(db.GetCards(10).ToList()), ball);
            var second = new Player("Leo", Squad.GetRandomSquad(db, "M", "3-2-5"),
                                    new Hand(db.GetCards(10).ToList()), ball);
            return Tuple.Create(first, second);
        }

        [Test]
        public void CheckMovement() {
            Ball ball = new Ball();
            var players = GetPlayers(ball);
            ball.Move();
            Assert.True(ball.BallPlace == ZoneType.ATT);
            Assert.True(ball.IsOwner(players.Item1.Team));
            Assert.False(players.Item2.Team.HasBall);
        }

        [Test]
        public void CheckInterception() {
            Ball ball = new Ball();
            var players = GetPlayers(ball);
            ball.Move();
            ball.Intercept(players.Item2.Team);
            Assert.True(ball.BallPlace == ZoneType.DEF);
            Assert.True(ball.IsOwner(players.Item2.Team));
            Assert.False(players.Item1.Team.HasBall);
        }
    }
}
