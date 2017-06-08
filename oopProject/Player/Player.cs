namespace oopProject
{
    public class Player
    {
        public readonly string Name;

        public Team Team { get; set; }
        
        public int Score { get; private set; }

        public Player(string name, Squad squad, Hand hand, Ball ball){
            Name = name;
            Team = new Team(squad, hand, ball);
        }

        public void IncreaseScore(int points) => Score += points;

        public string PrintTeam(ZoneType ballPlace) {
            return $"Player {Name}\nSquad is:\n{Team.Squad.Print(ballPlace, Team.HasBall)}";
        } 

        public override bool Equals(object obj)
        {
            var player = obj as Player;
            return player.Team.Squad.Formation.Equals(Team.Squad.Formation) && player.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
