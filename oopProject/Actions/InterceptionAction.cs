﻿namespace oopProject
{
    public class InterceptionAction : Action<EnemyParameters>
    {
        public override bool IsAvailable => !Game.CurrentPlayer.Team.HasBall;

        public override int Value => 30;

        public override string Explanation => "Try to intercept the ball";

        public override bool AreSuitable(EnemyParameters parameters) => true;

        public override bool Execute(EnemyParameters parameters)
        {
            var team = Game.CurrentPlayer.Team;
            var result = this.SuccessfulOperation(parameters.Enemy, z => z.InterceptPower(), team, 
                                                  z => z.DefendPower());
            if (result)
            {
                parameters.Enemy.Ball.InterceptedBy(team);
                WasSuccessfullyExecuted = true;
            }
            return WasSuccessfullyExecuted;
        }

        public override void Accept(ISuccess success) => success.Apply(this, WasSuccessfullyExecuted);
    }
}
