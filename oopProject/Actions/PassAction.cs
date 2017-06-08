using System;

namespace oopProject
{
    public class PassAction : Action<EnemyParameters>
    {
        public override string Explanation => "Pass the ball to the next zone";

        public override int Value => 25;

        public override bool IsAvailable => Game.CurrentPlayer.Team.HasBall && 
                                            Game.CurrentPlayer.Team.Ball.Place != ZoneType.ATT;

        public override bool AreSuitable(EnemyParameters parameters) => true;

        public override bool Execute(EnemyParameters parameters)
        {
            var team = Game.CurrentPlayer.Team;
            var result = this.SuccessfulOperation(team, z => z.PassPower(), parameters.Enemy, z => z.DefendPower());
            if (result)
            {
                team.Ball.Move();
                WasSuccessfullyExecuted = true;
            }
            else
                team.Ball.InterceptedBy(parameters.Enemy);
            return WasSuccessfullyExecuted;
        }

        public override void Accept(ISuccess success) => success.Apply(this, WasSuccessfullyExecuted);
    }
}
