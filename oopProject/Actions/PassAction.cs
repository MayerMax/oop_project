using System;

namespace oopProject
{
    class PassAction : IAction
    {
        private Team team;

        public PassAction(Team team)
        {
            this.team = team;
        }

        public string Explanation => "Pass the ball to the next zone";

        public bool IsAvailable => team.HasBall && team.Ball.BallPlace != ZoneType.ATT;

        public bool AreSuitable(IParameters parameters)
        {
            return parameters is PassParameters;
            // ???
        }

        public bool Execute(IParameters parameters)
        {
            var passParameters = parameters as PassParameters;
            if (SuccessfulPass(passParameters))
            {
                team.Ball.Move();
                return true;
            }
            return false;
        }

        private bool SuccessfulPass(PassParameters parameters)
        {
            var ballZone = team.Ball.BallPlace;
            var ballZonePower = team.Squad.GetZonePower(ballZone);
            var enemyOppositeZonePower = parameters.Enemy.Squad.GetZonePower(Ball.Transitions[ballZone]);
            return ballZonePower > enemyOppositeZonePower;
            //add random factor
        }
    }
}
