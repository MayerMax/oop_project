using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class SwapCard : IAction<SwapParameters>
    {
        private Team team;

        public SwapCard(Team team) {
            this.team = team;
        }

        public void Execute(SwapParameters parameters)
        {
            team.SubstitutionFromHandToSquad(parameters.OldCardZone,
                parameters.OldCardPosition, parameters.NewCard, parameters.NewCardPosition);
        }

        public string Explanation => "Swap cards";

        public bool IsAvailable => team.Hand.Any && team.Squad.Any;
    }
}
