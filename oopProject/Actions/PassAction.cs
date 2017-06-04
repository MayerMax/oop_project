using System;

namespace oopProject
{
    class PassAction : Action
    {
        private Team team;
        private EnemyParameters parameters;

        public PassAction(Team team)
        {
            this.team = team;
        }

        public override string Explanation => "Pass the ball to the next zone";

        public override bool IsAvailable => team.HasBall && team.Ball.BallPlace != ZoneType.ATT;

        public override bool SetSuitable(IParameters parameters)
        {
            this.parameters = SetParameters<EnemyParameters>(parameters);
            return true;
            
        }

        public override bool Execute()
        {
            CheckParameters(parameters);

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
