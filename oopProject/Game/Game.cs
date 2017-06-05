using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Game
    {
        public static readonly int PLAYERS_AMOUNT = 2;

        private FootballDatabase db;
        private List<PlayerController> players;
        private Ball ball;

        private int currentPlayerIdx;

        public readonly Deck Deck;
        public Player CurrentPlayer => players[currentPlayerIdx].Player;

        public IEnumerable<Action> GetActions()
            => players[currentPlayerIdx].Actions.Get();
        
        public IEnumerable<Player> GetOpponents => players.Select(c => c.Player)
                                                          .Where(p => p != CurrentPlayer);

        public string Score {
            get {
                var names = string.Join("vs ", players.Select(f => f.Player.Team.Squad.Name));
                var scores = string.Join(" : ", players.Select(f => f.Player.Score));
                return $"{names},{scores}";
                }
        }

        public Game()
        {
            db = new FootballDatabase(new MongoDatabase());
            players = new List<PlayerController>();
            ball = new Ball();
            Deck = new Deck(db);
        }

        private void Next() 
            =>currentPlayerIdx = (currentPlayerIdx + 1) % players.Count;
        
        private void Round() {

        }

        public void Run() {
            // TODO
        }
        public void AddPlayer(string name, string squadFormation, string squadName)
        {
            if (!Squad.ValidateSquad(squadFormation))
                throw new ArgumentException("Incorrect squad given!");
            var squad = Squad.GetRandomSquad(db, squadName, squadFormation);
            var player = new Player(name, squad, new Hand(db.GetCards(10).ToList()), ball);
            players.Add(new PlayerController(player));
        }

        private class PlayerController
        {
            public readonly ActionHolder Actions;
            public readonly Player Player;

            public PlayerController(Player player)
            {
                Player = player;
                Actions = new ActionHolder(ActionHolder.GetAllActionTypes());
                Actions.SetToPlayer(Player);
            }
        }
    }    
}