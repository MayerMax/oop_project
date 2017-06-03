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
                parameters.OldCardPosition, parameters.NewCard, parameters.NewCardPosition);
            return true;
        }

        public override bool SetSuitable(IParameters parameters)
        {
            parameters = SetParameters<SwapParameters>(parameters);
            return true;
            // REDO 
        }

        public override string Explanation => "Swap cards";

        public override bool IsAvailable => team.Hand.Any && team.Squad.Any;
    }
}
