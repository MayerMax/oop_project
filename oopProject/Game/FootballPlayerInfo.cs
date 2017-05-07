using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class FootballPlayerInfo: PlayerInfo
    {
        public FootballPlayerInfo(Dictionary<string, string> attributes) : base(attributes) { }

        public readonly List<string> DEF = new List<string>
        { "Skill_Moves","Ball_Control", "Dribbling",
           "Marking", "Standing_Tackle", "Sliding_Tackle"};

        public readonly List<string> MID = new List<string> {
            "Skill_Moves","Ball_Control", "Dribbling",
            "Reactions", "Interceptions", "Vision",
            "Composure", "Crossing", "Short_Pass", "Long_Pass"};

        public readonly List<string> ATT = new List<string>() {
            "Skill_Moves","Ball_Control", "Dribbling",
            "Composure", "Heading", "Finishing", "Long_Shots",
            "Penalties", "Shot_Power"};

        public readonly List<string> GENERAL = new List<string>() {
            "Stamina", "Balance", "Agility", "Strength"};




    }
}
