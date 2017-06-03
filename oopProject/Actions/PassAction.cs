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

            if (SuccessfulPass())
            {
                team.Ball.Move();
                return true;
            }
            return false;
        }

        private bool SuccessfulPass()
        {
            var ballZone = team.Ball.BallPlace;
            var ballZonePower = team.Squad.GetZonePower(ballZone);
            var enemyOppositeZonePower = parameters.Enemy.Squad.GetZonePower(Ball.Transitions[ballZone]);
            return ballZonePower > enemyOppositeZonePower;
            //add random factor
        }
    }
}
