using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class ActionHolder
    {
        private List<Type> actionTypes;
        private List<IAction<IParameters>> actions;

        public ActionHolder(List<Type> acts) {
            actionTypes = acts;       
            actions = new List<IAction<IParameters>>();
        }

        public void SetToPlayer(Player player) {
            var playerAttributes = player.GetType()
                .GetProperties()
                .Select(s => s.PropertyType)
                .ToList();

            foreach (var actionType in actionTypes) {
                var constructor = actionType.GetConstructors().First();
                var types = constructor
                    .GetParameters()
                    .Select(param => param.ParameterType)
                    .ToList();

                var needed = playerAttributes.Where(attrib => types.Contains(attrib)).ToList();
                var playerElements =
                     player
                    .GetType()
                    .GetProperties()
                    .Where(prop => needed.Contains(prop.PropertyType))
                    .Select(prop => prop.GetValue(player))
                    .ToArray();
                var c = (IAction<IParameters>)constructor.Invoke(playerElements);
                // DOES NOT WORK
                //BULLSHIT
            }

        }

        public IEnumerable<IAction<IParameters>> Get() {
            foreach (var act in actions)
                yield return act;
        }

    }
}
