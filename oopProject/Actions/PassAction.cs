using System;

namespace oopProject
{
    class PassAction : IAction
    {
        private Player player;

        public PassAction(Player player)
        {
            this.player = player;
        }

        public string Explanation => "Pass the ball to the next zone";

        public bool IsAvailable => player.HasBall && player.Ball.BallPlace != ZoneType.ATT;

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
                player.Ball.Move();
                return true;
            }
            return false;
        }

        private bool SuccessfulPass(PassParameters parameters)
        {
            var ballZone = player.Ball.BallPlace;
            var ballZonePower = player.Team.Squad.GetZonePower(ballZone);
            var enemyOppositeZonePower = parameters.Enemy.Team.Squad.GetZonePower(Ball.Transitions[ballZone]);
            return ballZonePower > enemyOppositeZonePower;
            //add random factor
        }
    }
}
