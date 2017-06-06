using System;

namespace oopProject
{
    public class PassAction : Action
    {
        private EnemyParameters parameters;

        public override string Explanation => "Pass the ball to the next zone";

        public override bool IsAvailable => game.CurrentPlayer.Team.HasBall && 
                                            game.CurrentPlayer.Team.Ball.Place != ZoneType.ATT;

        public override bool SetSuitable(IParameters parameters)
        {
            this.parameters = SetParameters<EnemyParameters>(parameters);
            return true;
            
        }

        public override bool Execute()
        {
            CheckParameters(parameters);

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
