using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class PressureAction : Action
    {
        private Team team;
        private PressureParameters parameters;

        public PressureAction(Team team)
        {
            this.team = team;
        }

        public override bool IsAvailable => team.Squad.Any;

        public override string Explanation => "Attacking an opponent zone, aiming to put off opponent's players";

        public override bool Execute()
        {
            CheckParameters(parameters);
            /////
            return true;
        }

        public override bool SetSuitable(IParameters parameters)
        {
            this.parameters = SetParameters<PressureParameters>(parameters);
            return team.Squad.AnyZone(this.parameters.Zone.Type);
        }
    }
}
