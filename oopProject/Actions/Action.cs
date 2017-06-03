using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    abstract class Action
    {
        public abstract bool IsAvailable { get; }
        public abstract string Explanation { get; }


        public abstract bool SetSuitable(IParameters parameters);
        public abstract bool Execute();


        protected T SetParameters<T>( IParameters parameters) where T :class {
            var converted = parameters as T;
            if (converted == null)
                throw new ArgumentException("Invalid parameters");
            return converted;
        }

        protected void CheckParameters(IParameters parameters) {
            if(parameters == null)
                throw new InvalidOperationException("Parameters weren't set");
        }
         
    }

    static class ActionExtensions
    {
        public static bool SuccessfulOperation(this Action action, Team curTeam, Func<Zone, double> currentPlayerFunc, 
                                               Team anotherTeam, Func<Zone, double> opponentFunc)
        {
            var ballZone = curTeam.Ball.BallPlace;
            var ballZonePower = curTeam.Squad.GetZonePower(ballZone, currentPlayerFunc);
            var enemyOppositeZonePower = anotherTeam.Squad.GetZonePower(Ball.Transitions[ballZone],
                                                                             opponentFunc);
            return ballZonePower > enemyOppositeZonePower;
        }
    }
}
