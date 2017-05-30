using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Player
    {
        public readonly string Name;
        public readonly string Formation;

        public Team Team { get; set; }
        public BonusHolder BonusHolder { get; set; }

        public bool HasBall { get { return ball.Owner.Equals(this); } }
        private Ball ball;

        public Player(FootballDatabase db, string name, string formation, Ball b){
            if (!Squad.ValidateSquad(formation))
                throw new ArgumentException("Incorrect squad given!");

            Name = name;
            Formation = formation;
            var filledSquad = FillSquadWithRandom(formation, db);
            var hand = new Hand(db.GetCards(10).ToList()); // Left it free for now
            var squad = new Squad(name, db.GetCardOfType(ZoneType.GK), filledSquad);
            Team = new Team(squad, hand);
            BonusHolder = new BonusHolder();
            ball = b;
            ball.AddObserver(this);
            
        }

        public string PrintTeam() {
            return $"Player {Name}\nSquad is:\n{Team.Squad.Print()}";
        } 

        private Dictionary<ZoneType, List<FootballCard>> FillSquadWithRandom(string formation, FootballDatabase db) {
            int[] eachzoneSize = formation.Split('-').Select(elem => int.Parse(elem)).ToArray();
            var resultFiller = new Dictionary<ZoneType, List<FootballCard>>();
            resultFiller.Add(ZoneType.DEF, db.GetCards(eachzoneSize[0]).ToList());
            resultFiller.Add(ZoneType.MID, db.GetCards(eachzoneSize[1]).ToList());
            resultFiller.Add(ZoneType.ATT, db.GetCards(eachzoneSize[2]).ToList());

           return resultFiller;
        }

        public override bool Equals(object obj)
        {
            var player = obj as Player;

            return player.Formation.Equals(Formation) && player.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
