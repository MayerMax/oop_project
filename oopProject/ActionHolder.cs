using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class ActionHolder
    {
        private List<Type> actionTypes;
        private List<IAction> actions;

        public ActionHolder(List<Type> acts) {
            actionTypes = acts;       
            actions = new List<IAction>();
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
                    .ToList();
                if (types.Contains(player.GetType()))
                    playerElements.Add(player);
                actions.Add((IAction)constructor.Invoke(playerElements.ToArray()));
            }
        }

        public IEnumerable<IAction> Get() {
            foreach (var act in actions)
                yield return act;
        }

    }
}
