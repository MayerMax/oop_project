using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void IncreaseScore() => Score += 1;
        

        public string PrintTeam() {
            return $"Player {Name}\nSquad is:\n{Team.Squad.Print()}";
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
