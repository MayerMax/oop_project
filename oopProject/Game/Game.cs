using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class Game
    {
        public static readonly int PLAYERS_AMOUNT = 2;

        private IFootballDatabase db;
        private List<Player> players;
        private Ball ball;

        private int currentPlayerIdx;

        public readonly Deck Deck;
        public Player CurrentPlayer => players[currentPlayerIdx];
        
        public IEnumerable<Player> GetOpponents => players.Where(p => p != CurrentPlayer);

        public string Score {
            get {
                var names = string.Join("vs ", players.Select(f => f.Team.Squad.Name));
                var scores = string.Join(" : ", players.Select(f => f.Score));
                return $"{names},{scores}";
                }
        }

        public Game(IFootballDatabase database, Ball ball)
        {
            db = database;
            players = new List<Player>();
            this.ball = ball;
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
            players.Add(new Player(name, squad, new Hand(db.GetCards(10).ToList()), ball));
        }

        public Game AddPlayer(Player player)
        {
            players.Add(player);
            return this;
        }
    }    
}