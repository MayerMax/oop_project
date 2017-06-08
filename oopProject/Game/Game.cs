using System;
using System.Collections.Generic;
using System.Linq;

namespace oopProject
{
    public class Game
    {
        public static readonly int PLAYERS_AMOUNT = 2;
        public static readonly int MOVES_AMOUNT = 45;

        private IFootballDatabase db;
        private List<Player> players;
        private Ball ball;
        private ISuccess success;

        private int currentPlayerIdx;

        public readonly Deck Deck;
        public int MovesLeft { get; private set; }

        public Player CurrentPlayer => players[currentPlayerIdx];
        public string BallOwner => ball.Owner;
        public ZoneType BallPlace => ball.Place;
        public string Message => success.Message;
        public string Winner => players.OrderByDescending(p => p.Score).First().Name;

        public IEnumerable<Player> GetOpponents => players.Where(p => p != CurrentPlayer);
        public string Score {
            get {
                var names = string.Join(" vs ", players.Select(f => f.Team.Squad.Name));
                var scores = string.Join(" : ", players.Select(f => f.Score));
                return $"{names}, {scores}";
                }
        }

        public Game(IFootballDatabase database)
        {
            db = database;
            players = new List<Player>();
            ball = new Ball();
            Deck = new Deck(db);
            success = new Success(this);
            MovesLeft = MOVES_AMOUNT; 
        }

        private void Next() 
            =>currentPlayerIdx = (currentPlayerIdx + 1) % players.Count;

        public void Turn(Tuple<IAction, IParameters> executionPair) {
            if (IsEnd)
                throw new GameEndException();
            executionPair.Item1.Execute(executionPair.Item2);
            executionPair.Item1.Accept(success);
            MovesLeft--;
            Next();
        }

        public bool IsEnd => (MovesLeft == 0) || (!CurrentPlayer.Team.Squad.Any);
              
           
        public void AddPlayer(string name, string squadFormation, string squadName)
        {
            ValidateFormation(squadFormation);
            var squad = Squad.GetRandomSquad(db, squadName, squadFormation);
            players.Add(new Player(name, squad, new Hand(db.GetCards(10).ToList()), ball));
        }

        public Game AddPlayer(Player player)
        {
            ValidateFormation(player.Team.Squad.Formation);
            players.Add(player);
            return this;
        }

        private void ValidateFormation(string formation)
        {
            if (!Squad.ValidateSquad(formation))
                throw new ArgumentException("Incorrect squad given!");
        }
    }

    public class GameEndException : InvalidOperationException {

    }    
}