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
        private Deck deck;
        private Ball ball;

        public Game()
        {
            db = new FootballDatabase(new MongoDatabase());
            players = new List<PlayerController>();
            ball = new Ball();
            deck = new Deck(db);
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