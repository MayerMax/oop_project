using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class SwapAction : Action<SwapParameters>
    {
        public override bool Execute(SwapParameters parameters)
        {
            game.CurrentPlayer.Team.SubstitutionFromHandToSquad(parameters.OldCardZone,
                parameters.CardPosition, parameters.NewCardPosition);
            wasSuccessfullyExecuted = true;
            return wasSuccessfullyExecuted;
        }

        public override bool AreSuitable(SwapParameters parameters)
        {
            var team = game.CurrentPlayer.Team;
            var isActive = team.Squad.IsActive(parameters.OldCardZone, parameters.CardPosition);
            return isActive && team.Hand.Contains(parameters.NewCardPosition);
        }

        public override string Explanation => "Swap cards";

        public override int Value => 5;

        public override bool IsAvailable => game.CurrentPlayer.Team.Hand.Any && 
                                            game.CurrentPlayer.Team.Squad.Any;

        public override void Accept(ISuccess success) => success.Apply(this, wasSuccessfullyExecuted);
    }
}
