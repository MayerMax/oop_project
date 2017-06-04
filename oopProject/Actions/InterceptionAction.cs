using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class InterceptionAction : Action
    {
        private Team team;
        private EnemyParameters parameters;

        public InterceptionAction(Team team)
        {
            this.team = team;
        }

        public override bool IsAvailable => !team.HasBall;

        public override string Explanation => "Try to intercept the ball";

        public override bool SetSuitable(IParameters parameters)
        {
            this.parameters = SetParameters<EnemyParameters>(parameters);
            return true;
        }

        public override bool Execute()
        {
            CheckParameters(parameters);

            var result = this.SuccessfulOperation(team, z => z.InterceptPower(), parameters.Enemy, 
                                                        z => z.DefendPower());
            if (result)
            {
                parameters.Enemy.Ball.InterceptedBy(team);
                return true;
            }
            return false;
        }
    }
}
