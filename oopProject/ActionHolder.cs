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
        private List<Action> actions;

        public ActionHolder(List<Type> acts) {
            actionTypes = acts;       
            actions = new List<Action>();
        }

        public void SetToPlayer(Player player) {
            var playerAttributes = player.GetType()
                .GetProperties()
                .Select(s => s.PropertyType)
                .ToList();

            foreach (var actionType in actionTypes) {
               try
                {
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

                    actions.Add((Action)constructor.Invoke(playerElements.ToArray()));
                }
                catch (InvalidOperationException) {
                    continue;
                }

            }
        }

        public IEnumerable<Action> Get() {
            foreach (var act in actions)
                yield return act;
        }

        public T Get<T>() where T : Action
        {
            foreach (var action in actions){
                var converted = action as T;
                if (converted != null)
                    return converted;
            }
            return null;
        }

        public static List<Type> GetAllActionTypes()
        {
            var type = typeof(Action);
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => type.IsAssignableFrom(p)).ToList();
        }
    }
}
