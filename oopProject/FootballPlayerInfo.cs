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

        private static readonly List<string> DEF = new List<string>
        { "Skill_Moves","Ball_Control", "Dribbling",
           "Marking", "Standing_Tackle", "Sliding_Tackle"};

        private static readonly List<string> MID = new List<string> {
            "Skill_Moves","Ball_Control", "Dribbling",
            "Reactions", "Interception", "Vision",
            "Composure", "Crossing", "Short_Pass", "Long_Pass"};

        private static readonly List<string> ATT = new List<string>() {
            "Skill_Moves","Ball_Control", "Dribbling",
            "Composure", "Heading", "Finishing", "Long_Shots",
            "Penalty", "Shot_Power"};

        private static readonly List<string> POS = new List<string>() {
            "Stamina", "Balance", "Agility", "Strength"};




    }
}
