using System;

namespace oopProject
{
    public abstract class Action<T> : IAction
        where T : class, IParameters
    {
        public abstract bool IsAvailable { get; }
        public abstract string Explanation { get; }
        public abstract int Value { get; }

        public abstract bool AreSuitable(T parameters);
        public abstract bool Execute(T parameters);
        public abstract void Accept(ISuccess success);

        protected Game Game { get; private set; }
        protected bool WasSuccessfullyExecuted { get; set; }

        public void SetUp(Game game) => Game = game;

        public bool Execute(IParameters parameters)
        {
            WasSuccessfullyExecuted = false;
            var converted = CheckParameters(parameters);
            return Execute(converted);
        }

        protected T CheckParameters(IParameters parameters)
        {
            var converted = parameters as T;
            if (converted == null || !AreSuitable(converted))
                throw new ArgumentException("Invalid parameters");
            return converted;
        }
    }

    public static class ActionExtensions
    {
        public static bool SuccessfulOperation(this IAction action, Team curTeam, Func<Zone, double> currentPlayerFunc, 
                                               Team anotherTeam, Func<Zone, double> opponentFunc)
        {
            ////
            var ballZone = curTeam.Ball.Place;
            var ballZonePower = curTeam.Squad.GetZonePower(ballZone, currentPlayerFunc);
            var enemyOppositeZonePower = anotherTeam.Squad.GetZonePower(Ball.Transitions[ballZone],
                                                                             opponentFunc);
            return ballZonePower > enemyOppositeZonePower;
        }
    }
}
