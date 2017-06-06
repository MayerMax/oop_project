using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class SwapAction : Action
    {
        private SwapParameters parameters;

        public override bool Execute()
        {
            game.CurrentPlayer.Team.SubstitutionFromHandToSquad(parameters.OldCardZone,
                parameters.CardPosition, parameters.NewCardPosition);
            wasSuccessfullyExecuted = true;
            return wasSuccessfullyExecuted;
        }

        public override bool SetSuitable(IParameters parameters)
        {
            this.parameters = SetParameters<SwapParameters>(parameters);
            var team = game.CurrentPlayer.Team;
            var isActive = team.Squad.IsActive(this.parameters.OldCardZone, this.parameters.CardPosition);
            return isActive && team.Hand.Contains(this.parameters.NewCardPosition);
        }

        public override string Explanation => "Swap cards";

        public override bool IsAvailable => game.CurrentPlayer.Team.Hand.Any && 
                                            game.CurrentPlayer.Team.Squad.Any;

        public override void Accept(ISuccess success) => success.Apply(this, wasSuccessfullyExecuted);
    }
}
