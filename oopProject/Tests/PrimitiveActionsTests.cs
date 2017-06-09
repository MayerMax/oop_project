using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework;

namespace oopProject
{
    static class Parameters
    {
        public static StandardKernel Container = Parameters.GetContainer();

        public static Tuple<Player, Player> GetPlayers(IBall ball)
        {
            var db = Container.Get<IFootballDatabase>();
            var first = new Player("Max", Squad.GetRandomSquad(db, "N", "3-4-3"),
                                   new Hand(db.GetCards(10).ToList()), ball);
            var second = new Player("Leo", Squad.GetRandomSquad(db, "M", "3-2-5"),
                                    new Hand(db.GetCards(10).ToList()), ball);
            return Tuple.Create(first, second);

        }

        public static Player GeneratePlayer(IBall ball, string name = "Leo")
        {
            var db = Container.Get<IFootballDatabase>();
            return new Player(name, Squad.GetRandomSquad(db, "N", "3-4-3"),
                                   new Hand(db.GetCards(10).ToList()), ball);
        }

        private static StandardKernel GetContainer()
        {
            var container = new StandardKernel();
            container.Bind<IDatabase>().To<MongoDatabase>();
            container.Bind<IFootballDatabase>().To<FootballDatabase>();
            container.Bind<IBall>().To<Ball>();
            container.Bind<IAction>().To<GetFromDeckAction>();
            container.Bind<IAction>().To<InterceptionAction>();
            container.Bind<IAction>().To<PassAction>();
            container.Bind<IAction>().To<PressureAction>();
            container.Bind<IAction>().To<ShootAction>();
            container.Bind<IAction>().To<SwapAction>();
            return container;
        }

    }

    [TestFixture]
    class BallActionsTesting
    {
        [Test]
        public void CheckMovement()
        {
            Ball ball = Parameters.Container.Get<Ball>();
            var players = Parameters.GetPlayers(ball);
            ball.Move();
            Assert.True(ball.Place == ZoneType.ATT);
            Assert.True(ball.IsOwner(players.Item1.Team));
            Assert.False(players.Item2.Team.HasBall);
        }

        [Test]
        public void CheckInterception()
        {
            Ball ball = Parameters.Container.Get<Ball>();
            var players = Parameters.GetPlayers(ball);
            ball.Move();
            ball.InterceptedBy(players.Item2.Team);
            Assert.True(ball.Place == ZoneType.DEF);
            Assert.True(ball.IsOwner(players.Item2.Team));
            Assert.False(players.Item1.Team.HasBall);
        }

        [TestFixture]
        class ShotActionTesting
        {

            [Test]
            public void MakeShotAndVerifyResults()
            {
                Ball ball = new Ball(whereToStart: ZoneType.ATT);
                var players = Parameters.GetPlayers(ball);
                var player = players.Item1;
                var opponent = players.Item2;

                ShootAction action = Parameters.Container.Get<ShootAction>();
                action.SetUp(Parameters.Container.Get<Game>().AddPlayer(player).AddPlayer(opponent));
                var shootParams = new EnemyParameters(opponent.Team);

                Assert.True(ball.Place == ZoneType.ATT);
                var executionStatus = action.Execute(shootParams);
                if (executionStatus)
                {
                    Assert.True(ball.Place == ZoneType.MID);
                    Assert.True(player.Team.HasBall == false);
                }
                else
                {

                    Assert.True(ball.IsOwner(opponent.Team));
                    Assert.True(ball.Place == ZoneType.DEF);
                }
            }
        }

    }

    [TestFixture]
    class PressureActionTesting
    {
        [Test]
        public void SuppressOpponent()
        {
            var ball = Parameters.Container.Get<Ball>();
            var players = Parameters.GetPlayers(ball);

            var player = players.Item1;
            var opponent = players.Item2;

            IAction action = Parameters.Container.Get<PressureAction>();
            action.SetUp(Parameters.Container.Get<Game>().AddPlayer(player).AddPlayer(opponent));
            var attackedZoneType = ZoneType.DEF;
            var attackingZone = player.Team.Squad[ZoneType.ATT];
            var defendingZone = opponent.Team.Squad[attackedZoneType];

            var opponetZoneRankings = defendingZone.GetCards().Select(f => f.Rank).ToList();
            var playerZoneRankings = attackingZone.GetCards().Select(f => f.Rank).ToList();

            var parameters = new PressureParameters(attackedZoneType, opponent.Team);

            var executed = action.Execute(parameters);
            if (executed)
            {
                var newOpponentsRanking = defendingZone.GetCards().Select(f => f.Rank).ToList();
                for (int idx = 0; idx < newOpponentsRanking.Count; idx++)
                {
                    Assert.True(newOpponentsRanking[idx] <= opponetZoneRankings[idx]);
                }
            }
            else
            {

                var newPlayersRanking = attackingZone.GetCards().Select(f => f.Rank).ToList();
                for (int idx = 0; idx < newPlayersRanking.Count(); idx++)
                {
                    Assert.True(newPlayersRanking[idx] <= playerZoneRankings[idx]);
                }
            }
        }
    }

    [TestFixture]
    class GetFromDeckActionTest
    {

        [Test]
        public void TransitionIsMade()
        {
            var ball = Parameters.Container.Get<Ball>();
            Deck deck = new Deck(Parameters.Container.Get<IFootballDatabase>());
            var player = Parameters.GeneratePlayer(ball);

            IAction action = Parameters.Container.Get<GetFromDeckAction>();
            action.SetUp(Parameters.Container.Get<Game>().AddPlayer(player));

            var deckParameters = new GetFromDeckParameters(deck);
            Assert.True(action.IsAvailable);

            var executed = action.Execute(deckParameters);
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
            var ball = Parameters.Container.Get<Ball>();
            var players = Parameters.GetPlayers(ball);
            var first = players.Item1;
            var second = players.Item2;
            IAction action = Parameters.Container.Get<PassAction>();
            action.SetUp(Parameters.Container.Get<Game>().AddPlayer(first).AddPlayer(second));

            Assert.True(first.Team.HasBall);
            Assert.AreEqual(first.Team.Ball.Place, ZoneType.MID);

            var parameters = new EnemyParameters(second.Team);

            if (action.Execute(parameters))
                Assert.AreEqual(first.Team.Ball.Place, ZoneType.ATT);
            else
            {
                Assert.True(second.Team.HasBall);
                Assert.AreEqual(second.Team.Ball.Place, ZoneType.MID);
            }
        }
    }

    [TestFixture]
    class SwapActionTest
    {
        [Test]
        public void CheckSwap()
        {
            var ball = Parameters.Container.Get<Ball>();
            Deck deck = new Deck(Parameters.Container.Get<IFootballDatabase>());
            var player = Parameters.GeneratePlayer(ball);
            IAction action = Parameters.Container.Get<SwapAction>();
            action.SetUp(Parameters.Container.Get<Game>().AddPlayer(player));
            Assert.True(action.IsAvailable);

            var invalidParameters = new SwapParameters(12, ZoneType.MID, 150);
            Assert.Throws(typeof(ArgumentException), () => action.Execute(invalidParameters));

            var position = 3;
            var zone = ZoneType.MID;
            var parameters = new SwapParameters(position, zone, 2);

            var swappedCard = player.Team.Squad[zone][position].Card;
            var substitution = player.Team.Hand[parameters.NewCardPosition];

            action.Execute(parameters);
            Assert.AreEqual(player.Team.Squad[zone][position].Card, substitution);
            Assert.True(player.Team.Hand.Contains(swappedCard));
            Assert.False(player.Team.Hand.Contains(substitution));
        }
    }

    [TestFixture]
    class InterceptionActionTest
    {
        [Test]
        public void CheckIntercept()
        {
            var ball = Parameters.Container.Get<IBall>();
            var players = Parameters.GetPlayers(ball);
            var first = players.Item1;
            var second = players.Item2;
            var game = Parameters.Container.Get<Game>().AddPlayer(first).AddPlayer(second);
            IAction action = Parameters.Container.Get<InterceptionAction>();
            action.SetUp(game);

            var ballPlace = ZoneType.MID;
            Assert.True(first.Team.HasBall);
            Assert.AreEqual(first.Team.Ball.Place, ballPlace);

            var parameters = new EnemyParameters(first.Team);

            if (action.Execute(parameters))
            {
                Assert.True(second.Team.HasBall);
                Assert.AreEqual(second.Team.Ball.Place, ballPlace);
            }
            else
            {
                Assert.True(first.Team.HasBall);
                Assert.AreEqual(first.Team.Ball.Place, ballPlace);
            }
        }
    }
}

