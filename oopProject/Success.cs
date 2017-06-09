using System;

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
        void Apply(SwapInSquadAction action, bool successful);
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
            var applier = SuccessAction(action, 
                g => $"{g.CurrentPlayer.Team.Hand.Peek.CardName} has been substituted");
            Apply(applier);
        }

        public void Apply(PressureAction action, bool successful)
        {
            var successPressing = SuccessAction(action, g => "Successful pressing!");
            var failurePressing = FailureAction(action, g => "You have been injured!");
            Apply(successPressing, failurePressing, successful);
        }

        public void Apply(GetFromDeckAction action, bool successful)
        {
            var applier = SuccessAction(action,
                g => $"Great, {g.CurrentPlayer.Team.Hand.Peek.CardName} was added to your Hand");
            Apply(applier);
        }

        public void Apply(InterceptionAction action, bool successful)
        {
            var successInterception = SuccessAction(action, g => "What a move! Ball is in your possession now");
            var failureInterception = FailureAction(action, g => "Unfortunately, you couldn't intercept the ball");
            Apply(successInterception, failureInterception, successful);
        }

        public void Apply(ShootAction action, bool successful)
        {
            var successShot = SuccessAction(action, g => "Congratulations! You've scored a goal! You're amazing!");
            var failureShot = FailureAction(action, g => $"Unlucky this time! Ball goes to {game.BallOwner}");
            Apply(successShot, failureShot, successful);
        }

        public void Apply(PassAction action, bool successful)
        {
            var successPass = SuccessAction(action, g => $"ball moves to {game.BallPlace}, Nice!");
            var failurePass = FailureAction(action, g => $"ball was intercepted by {game.BallOwner}");
            Apply(successPass, failurePass, successful);
        }

        public void Apply(SwapInSquadAction action, bool successful)
            => Apply(SuccessAction(action, g => "Swap completed!"));

        private System.Action<Game> SuccessAction(IAction action, Func<Game, string> message)
            => CreateAction(action, message, true);

        private System.Action<Game> FailureAction(IAction action, Func<Game, string> message)
            => CreateAction(action, message, false);

        private System.Action<Game> CreateAction(IAction action, Func<Game, string> message, bool successful) => 
            g =>
            {
                AddPointsToPlayer(action, successful);
                Message = message(g);
            };

        private void Apply(System.Action<Game> success, System.Action<Game> failure, bool successful) 
            => Apply(successful ? success : failure);

        private void Apply(System.Action<Game> action) => action(game);

        private void AddPointsToPlayer(IAction action, bool successful)
        {
            if (successful)
                game.CurrentPlayer.IncreaseScore(action.Value);
            else
                foreach (var opponent in game.GetOpponents)
                    opponent.IncreaseScore(action.Value / 2);
        }
    }
}
