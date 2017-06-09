using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class SwapInSquadAction : Action<SwapInSquadParameters>
    {
        public override int Value => 5;
        public override bool IsAvailable => Game.CurrentPlayer.Team.Squad.Count >= 2;
        public override string Explanation => "Swap players in the squad";

        public override bool AreSuitable(SwapInSquadParameters parameters)
        {
            var team = Game.CurrentPlayer.Team;
            return team.Squad.IsActive(parameters.First, parameters.FirstPos) &&
                   team.Squad.IsActive(parameters.Second, parameters.SecondPos);
        }

        public override bool Execute(SwapInSquadParameters parameters)
        {
            Game.CurrentPlayer.Team.SwapInSquad(parameters.First, parameters.FirstPos,
                                                parameters.Second, parameters.SecondPos);
            WasSuccessfullyExecuted = true;
            return WasSuccessfullyExecuted;
        }

        public override void Accept(ISuccess success) => success.Apply(this, WasSuccessfullyExecuted);
    }
}
