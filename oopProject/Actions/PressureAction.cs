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

        public override bool IsAvailable => Game.CurrentPlayer.Team.Squad.Any;

        public override int Value => 20;

        public override string Explanation => "Attacking an opponent zone, aiming to put off opponent's players";

        public override bool Execute(PressureParameters parameters)
        {
            var zone = Game.CurrentPlayer.Team.Squad[Ball.Transitions[parameters.ZoneType]];
            var pressure = zone.PressurePower();
            var opponentZone = parameters.Enemy.Squad[parameters.ZoneType];
            var opponentPressure = opponentZone.PressurePower();
            if (pressure >= opponentPressure)
            {
                DecreaseRankings(opponentZone, 10, 30);
                WasSuccessfullyExecuted = true;
            }
            else
                DecreaseRankings(zone, 10, 15);
            return WasSuccessfullyExecuted;
        }

        private void DecreaseRankings(Zone zone, int minDecrease, int maxDecrease) {
            zone.DecreaseRandomCardRank(rand.Next(minDecrease, maxDecrease));
            zone.RemoveDeadCards();
        }

        public override bool AreSuitable(PressureParameters parameters)
        {
            var team = Game.CurrentPlayer.Team;
            return parameters.ZoneType != ZoneType.GK && 
                team.Squad.AnyZone(parameters.ZoneType) && team != parameters.Enemy;
        }

        public override void Accept(ISuccess success) => success.Apply(this, WasSuccessfullyExecuted);
    }
}
