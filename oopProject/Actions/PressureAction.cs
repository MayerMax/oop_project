using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class PressureAction : Action<PressureParameters>
    {
        private Random rand;

        public PressureAction()
        {
            rand = new Random();
        }

        public override bool IsAvailable => game.CurrentPlayer.Team.Squad.Any;

        public override string Explanation => "Attacking an opponent zone, aiming to put off opponent's players";

        public override bool Execute(PressureParameters parameters)
        {
            var zone = game.CurrentPlayer.Team.Squad[parameters.ZoneType];
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

        public override bool AreSuitable(PressureParameters parameters)
        {
            var team = game.CurrentPlayer.Team;
            return team.Squad.AnyZone(parameters.ZoneType) && team != parameters.Enemy;
        }

        public override void Accept(ISuccess success) => success.Apply(this, wasSuccessfullyExecuted);
    }
}
