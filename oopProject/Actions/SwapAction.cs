using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class SwapAction : IAction
    {
        private Team team;

        public SwapAction(Team team){
            this.team = team;
        }

        public bool Execute(IParameters parameters)
        {
            var swapParameters = parameters as SwapParameters;
            team.SubstitutionFromHandToSquad(swapParameters.OldCardZone,
                swapParameters.OldCardPosition, swapParameters.NewCard, swapParameters.NewCardPosition);
            return true;
        }

        public bool AreSuitable(IParameters parameters)
        {
            throw new NotImplementedException();
        }

        public string Explanation => "Swap cards";

        public bool IsAvailable => team.Hand.Any && team.Squad.Any;
    }
}
