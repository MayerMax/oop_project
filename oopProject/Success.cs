using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public interface ISuccess
    {
        string Message { get; }
        void Apply(PassAction action, bool isTrue);
        void Apply(ShootAction action, bool isTrue);
        void Apply(InterceptionAction action, bool isTrue);
        void Apply(SwapAction action, bool isTrue);
        void Apply(GetFromDeckAction action, bool isTrue);
        void Apply(PressureAction action, bool isTrue);
    }

    public class Success : ISuccess
    {
        private Game game;
        public Success(Game game)
        {
            this.game = game;
        }

        public string Message { get; private set; }


        public void Apply(SwapAction action, bool isTrue)
        {
            System.Action<Player> applier =
                p => Message = $"{p.Team.Hand.Peek.CardName} has been substituted";
            Apply(applier);
        }

        public void Apply(PressureAction action, bool isTrue)
        {
            System.Action<Player> successPressing = p => Message = "Successful pressing!";
            System.Action<Player> failurePressing = p => Message = "You have been injured!";
            Apply(successPressing, failurePressing, isTrue);
        }

        public void Apply(GetFromDeckAction action, bool isTrue)
        {
            System.Action<Player> applier = p => Message = $"Great, {p.Team.Hand.Peek.CardName} was added to your Hand";
            Apply(applier);
        }

        public void Apply(InterceptionAction action, bool isTrue)
        {
            System.Action<Player> successInterception = p => Message = $"What a move! Ball is in your possession now";
            System.Action<Player> failureInterception = p => Message = "Unfortunately, you couldn't intercept the ball";
            Apply(successInterception, failureInterception, isTrue);
        }

        public void Apply(ShootAction action, bool isTrue)
        {
            System.Action<Player> successShot = p =>
            {
                p.IncreaseScore();
                Message = "Congratulations! You've scored a goal! You're amazing!";
            };

            System.Action<Player> failureShot = p => Message = $"Unlucky this time! Ball goes to {p.Team.Ball.Owner}";

            Apply(successShot, failureShot, isTrue);
        }

        public void Apply(PassAction action, bool isTrue)
        {
            System.Action<Player> successPass = p => Message = $"ball moves to {p.Team.Ball.Place}, Nice!";
            System.Action<Player> failurePass = p => Message = $"ball was intercepted by {p.Team.Ball.Owner}";
            Apply(successPass, failurePass, isTrue);
        }

        private void Apply(System.Action<Player> success, System.Action<Player> failure, bool successful)
        {
            if (successful) Apply(success); else Apply(failure);
        }

        private void Apply(System.Action<Player> action) => action(game.CurrentPlayer);
    }
}
