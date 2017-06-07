using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class ShootAction : Action<EnemyParameters>
    {
        public override string Explanation => $"Make shoot if you're owning the ball and it is in {ZoneType.ATT}";

        public override int Value => 100;

        public override bool IsAvailable => game.CurrentPlayer.Team.HasBall && 
                                            game.CurrentPlayer.Team.Ball.Place == ZoneType.ATT;

        public override bool AreSuitable(EnemyParameters parameters)
            => !parameters.Enemy.HasBall;

        public override bool Execute(EnemyParameters parameters)
        {
            var team = game.CurrentPlayer.Team;
            var opponentKeeper = parameters.Enemy.Squad[ZoneType.GK].GetCards().First();
            var result = this.SuccessfulOperation(team, z => z.ShootPower(), 
                                                  parameters.Enemy, z => z.WithAdditionalPower(opponentKeeper));
            if (result)
            {
                team.Ball.Restart(parameters.Enemy);
                wasSuccessfullyExecuted = true;
            }
            else
                team.Ball.InterceptedBy(parameters.Enemy);
            return wasSuccessfullyExecuted;
        }

        public override void Accept(ISuccess success) => success.Apply(this, wasSuccessfullyExecuted);
    }
}
