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
        private Random rand;

        public PressureAction(Team team)
        {
            this.team = team;
            rand = new Random();
        }

        public override bool IsAvailable => team.Squad.Any;

        public override string Explanation => "Attacking an opponent zone, aiming to put off opponent's players";

        public override bool Execute()
        {
            CheckParameters(parameters);
            var zone = team.Squad[parameters.ZoneType];
            var pressure = zone.PressurePower();
            var opponentZone = parameters.Enemy.Squad[Ball.Transitions[parameters.ZoneType]];
            var opponentPressure = opponentZone.PressurePower();
            if (pressure >= opponentPressure)
            {
                DecreaseRankings(opponentZone, 10, 30);
                wasSuccessfullyExecuted = true;
            }
            else
                DecreaseRankings(zone, 10, 15);
            return wasSuccessfullyExecuted;
        }

        private void DecreaseRankings(Zone zone, int minDecrease, int maxDecrease) {
            zone.DecreaseRandomCardRank(rand.Next(minDecrease, maxDecrease));
            zone.RemoveDeadCards();
        }

        public override bool SetSuitable(IParameters parameters)
        {
            this.parameters = SetParameters<PressureParameters>(parameters);
            return team.Squad.AnyZone(this.parameters.ZoneType) && team != this.parameters.Enemy;
        }

        public override void Accept(ISuccess success) => success.Apply(this, wasSuccessfullyExecuted);
    }
}
