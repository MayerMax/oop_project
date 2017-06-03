using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace oopProject
{
    static class Parameters
    {
        public static FootballDatabase db = new FootballDatabase(new MongoDatabase());

        public static Tuple<Player, Player> GetPlayers(Ball ball)
        {
            var first = new Player("Max", Squad.GetRandomSquad(db, "N", "3-4-3"),
                                   new Hand(db.GetCards(10).ToList()), ball);
            var second = new Player("Leo", Squad.GetRandomSquad(db, "M", "3-2-5"),
                                    new Hand(db.GetCards(10).ToList()), ball);
            return Tuple.Create(first, second);

        }

        public static Tuple<Player, ActionHolder> GetRawPlayerController(Ball ball) {
            var player = new Player("Max", Squad.GetRandomSquad(db, "N", "3-4-3"),
                                   new Hand(db.GetCards(10).ToList()), ball);

            var actionHolder = new ActionHolder(ActionHolder.GetAllActionTypes());
            actionHolder.SetToPlayer(player);
            return Tuple.Create(player, actionHolder);
        }

    }

    [TestFixture]
    class BallActionsTesting
    {

        [Test]
        public void CheckMovement()
        {
            Ball ball = new Ball();
            var players = Parameters.GetPlayers(ball);
            ball.Move();
            Assert.True(ball.BallPlace == ZoneType.ATT);
            Assert.True(ball.IsOwner(players.Item1.Team));
            Assert.False(players.Item2.Team.HasBall);
        }

        [Test]
        public void CheckInterception()
        {
            Ball ball = new Ball();
            var players = Parameters.GetPlayers(ball);
            ball.Move();
            ball.Intercept(players.Item2.Team);
            Assert.True(ball.BallPlace == ZoneType.DEF);
            Assert.True(ball.IsOwner(players.Item2.Team));
            Assert.False(players.Item1.Team.HasBall);
        }

        // Pass
        //Shoot
        
    }

    [TestFixture]
    class GetFromDeckActionTest {

        [Test]
        public void TransitionIsMade() {
            Ball ball = new Ball();
            Deck deck = new Deck(Parameters.db);
            var controller = Parameters.GetRawPlayerController(ball);
            var player = controller.Item1;
            var holder = controller.Item2;

            var action = holder.Get<GetFromDeckAction>();

            var deckParameters = new GetFromDeckParameters(deck);
            Assert.True(action.IsAvailable);
            action.SetSuitable(deckParameters);

            var executed = action.Execute();
            Assert.True(executed);

            Assert.True(player.Team.Hand.Any);
            var card = player.Team.Hand[10];
            Console.WriteLine(card.ToString());
        }
    }

    [TestFixture]
    class PassActionTest
    {
        [Test]
        public void CheckPassTransition()
        {
            var ball = new Ball();
            var players = Parameters.GetPlayers(ball);
            var first = players.Item1;
            var second = players.Item2;
            var action = new PassAction(first.Team);

            Assert.True(first.Team.HasBall);
            Assert.AreEqual(first.Team.Ball.BallPlace, ZoneType.MID);

            var parameters = new EnemyParameters(second.Team);
            action.SetSuitable(parameters);

            if (action.Execute())
                Assert.AreEqual(first.Team.Ball.BallPlace, ZoneType.ATT);
            else
                Assert.AreEqual(first.Team.Ball.BallPlace, ZoneType.MID);
                // should be checked that another team has the ball now
        }
    }
}
    
