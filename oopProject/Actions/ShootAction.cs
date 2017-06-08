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

        public override bool IsAvailable => Game.CurrentPlayer.Team.HasBall && 
                                            Game.CurrentPlayer.Team.Ball.Place == ZoneType.ATT;

        public override bool AreSuitable(EnemyParameters parameters)
            => !parameters.Enemy.HasBall;

        public override bool Execute(EnemyParameters parameters)
        {
            var team = Game.CurrentPlayer.Team;
            var opponentKeeper = parameters.Enemy.Squad[ZoneType.GK].GetCards().First();
            var result = this.SuccessfulOperation(team, z => z.ShootPower(), 
                                                  parameters.Enemy, z => z.WithAdditionalPower(opponentKeeper));
            if (result)
            {
                team.Ball.Restart(parameters.Enemy);
                WasSuccessfullyExecuted = true;
            }
            else
                team.Ball.InterceptedBy(parameters.Enemy);
            return WasSuccessfullyExecuted;
        }

        public override void Accept(ISuccess success) => success.Apply(this, WasSuccessfullyExecuted);
    }
}
