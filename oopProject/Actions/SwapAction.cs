using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class SwapAction : Action
    {
        private Team team;
        private SwapParameters parameters;

        public SwapAction(Team team){
            this.team = team;
        }

        public override bool Execute()
        {
            team.SubstitutionFromHandToSquad(parameters.OldCardZone,
                parameters.CardPosition, parameters.NewCard);
            return true;
        }

        public override bool SetSuitable(IParameters parameters)
        {
            this.parameters = SetParameters<SwapParameters>(parameters);
            var isActive = team.Squad.IsActive(this.parameters.OldCardZone, this.parameters.CardPosition);
            return isActive && team.Hand.Contains(this.parameters.NewCard);
        }

        public override string Explanation => "Swap cards";

        public override bool IsAvailable => team.Hand.Any && team.Squad.Any;
    }
}
