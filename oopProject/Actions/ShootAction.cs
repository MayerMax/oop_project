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
        public ShootAction(Team team)
        {
            this.team = team;
        }

        public override string Explanation => $"Make shoot if you're owning the ball and it is in {ZoneType.ATT}";

        public override bool IsAvailable => team.HasBall && team.Ball.BallPlace == ZoneType.ATT;

        public override bool SetSuitable(IParameters parameters)
        {
            var enemyParams = parameters as EnemyParameters;
            if (enemyParams == null)
                return false;
            return !enemyParams.Enemy.HasBall;
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }


    }
}
