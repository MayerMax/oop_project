using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject.Actions
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

            var result = this.SuccessfulOperation(team, z => z.ShootPower(), parameters.Enemy, z => z.DefendPower());
            if (result)
            {
                team.Ball.Restart(parameters.Enemy);
                return true;
            }
            return false;
        }
    }
}
