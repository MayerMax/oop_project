namespace oopProject
{
    public interface ISuccess
    {
        string Message { get; }
        void Apply(PassAction action, bool successful);
        void Apply(ShootAction action, bool successful);
        void Apply(InterceptionAction action, bool successful);
        void Apply(SwapAction action, bool successful);
        void Apply(GetFromDeckAction action, bool successful);
        void Apply(PressureAction action, bool successful);
    }

    public class Success : ISuccess
    {
        private Game game;
        public Success(Game game)
        {
            this.game = game;
        }

        public string Message { get; private set; }


        public void Apply(SwapAction action, bool successful)
        {
            System.Action<Game> applier = g => 
            {
                AddPointsToPlayer(action);
                Message = $"{g.CurrentPlayer.Team.Hand.Peek.CardName} has been substituted";
            };
            Apply(applier);
        }

        public void Apply(PressureAction action, bool successful)
        {
            System.Action<Game> successPressing = g =>
            {
                AddPointsToPlayer(action);
                Message = "Successful pressing!";
            };
            System.Action<Game> failurePressing = g => Message = "You have been injured!";
            Apply(successPressing, failurePressing, successful);
        }

        public void Apply(GetFromDeckAction action, bool successful)
        {
            System.Action<Game> applier = g =>
                {
                    AddPointsToPlayer(action);
                    Message = $"Great, {g.CurrentPlayer.Team.Hand.Peek.CardName} was added to your Hand";
                };
            Apply(applier);
        }

        public void Apply(InterceptionAction action, bool successful)
        {
            System.Action<Game> successInterception = g =>
            {
                AddPointsToPlayer(action);
                Message = $"What a move! Ball is in your possession now";
            };
            System.Action<Game> failureInterception = g => Message = "Unfortunately, you couldn't intercept the ball";
            Apply(successInterception, failureInterception, successful);
        }

        public void Apply(ShootAction action, bool successful)
        {
            System.Action<Game> successShot = g =>
            {
                AddPointsToPlayer(action);
                Message = "Congratulations! You've scored a goal! You're amazing!";
            };

            System.Action<Game> failureShot = g => Message = $"Unlucky this time! Ball goes to {game.BallOwner}";

            Apply(successShot, failureShot, successful);
        }

        public void Apply(PassAction action, bool successful)
        {
            System.Action<Game> successPass = g =>
            {
                AddPointsToPlayer(action);
                Message = $"ball moves to {game.BallPlace}, Nice!";
            };
            System.Action<Game> failurePass = g => Message = $"ball was intercepted by {game.BallOwner}";
            Apply(successPass, failurePass, successful);
        }

        private void Apply(System.Action<Game> success, System.Action<Game> failure, bool successful)
        {
            Apply(successful ? success : failure);
        }

        private void Apply(System.Action<Game> action) => action(game);

        private void AddPointsToPlayer(IAction action) => game.CurrentPlayer.IncreaseScore(action.Value);
    }
}
