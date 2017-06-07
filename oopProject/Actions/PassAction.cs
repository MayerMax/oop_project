using System;

namespace oopProject
{
    public class PassAction : Action<EnemyParameters>
    {
        public override string Explanation => "Pass the ball to the next zone";

        public override bool IsAvailable => game.CurrentPlayer.Team.HasBall && 
                                            game.CurrentPlayer.Team.Ball.Place != ZoneType.ATT;

        public override bool AreSuitable(EnemyParameters parameters) => true;

        public override bool Execute(EnemyParameters parameters)
        {
            var team = game.CurrentPlayer.Team;
            var result = this.SuccessfulOperation(team, z => z.PassPower(), parameters.Enemy, z => z.DefendPower());
            if (result)
            {
                team.Ball.Move();
                wasSuccessfullyExecuted = true;
            }
            else
                team.Ball.InterceptedBy(parameters.Enemy);
            return wasSuccessfullyExecuted;
        }

        public override void Accept(ISuccess success) => success.Apply(this, wasSuccessfullyExecuted);
    }
}
