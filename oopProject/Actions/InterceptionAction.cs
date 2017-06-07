using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class InterceptionAction : Action<EnemyParameters>
    {
        public override bool IsAvailable => !game.CurrentPlayer.Team.HasBall;

        public override string Explanation => "Try to intercept the ball";

        public override bool AreSuitable(EnemyParameters parameters) => true;

        public override bool Execute(EnemyParameters parameters)
        {
            var team = game.CurrentPlayer.Team;
            var result = this.SuccessfulOperation(team, z => z.InterceptPower(), parameters.Enemy, 
                                                  z => z.DefendPower());
            if (result)
            {
                parameters.Enemy.Ball.InterceptedBy(team);
                wasSuccessfullyExecuted = true;
            }
            return wasSuccessfullyExecuted;
        }

        public override void Accept(ISuccess success) => success.Apply(this, wasSuccessfullyExecuted);
    }
}
