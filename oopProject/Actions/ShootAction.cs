using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class ShootAction : Action
    {
        private Team team;
        private EnemyParameters parameters;

        public ShootAction(Team team)
        {
            this.team = team;
        }

        public override string Explanation => $"Make shoot if you're owning the ball and it is in {ZoneType.ATT}";

        public override bool IsAvailable => team.HasBall && team.Ball.BallPlace == ZoneType.ATT;

        public override bool SetSuitable(IParameters parameters)
        {
            this.parameters = SetParameters<EnemyParameters>(parameters);
            return !this.parameters.Enemy.HasBall;
        }

        public override bool Execute()
        {
            CheckParameters(parameters);
            var opponentKeeper = parameters.Enemy.Squad[ZoneType.GK].GetCards().First();
            var result = this.SuccessfulOperation(team, z => z.ShootPower(), 
                                                  parameters.Enemy, z => z.WithAdditionalPower(opponentKeeper));
            if (result)
            {
                team.Ball.Restart(parameters.Enemy);
                return true;
            }
            team.Ball.InterceptedBy(parameters.Enemy);
            return false;
        }
    }
}
