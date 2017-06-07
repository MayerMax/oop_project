using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class ConsoleController
    {
        private Game game;
        private List<IAction> actions;

        public ConsoleController(Game game, List<IAction> actions)
        {
            this.game = game;
            this.actions = actions;
            for (int i = 0; i < Game.PLAYERS_AMOUNT; i++)
                InitPlayer();
        }

        private void RequireChoice()
        {
            int playerChoice = PlayerChoice();

        }

        private void ShowActions(List<IAction> actionsList)
        {
            for (int i = 0; i < actionsList.Count; i++)
            {
                var explanation = $"{i++}.{actionsList[i].Explanation}";
                if (actionsList[i].IsAvailable)
                    Console.WriteLine(explanation);
                else
                    Console.WriteLine($"{explanation} (Unavailable)");
            }
        }

        private int PlayerChoice()
        {
            ShowActions(actions);
            Console.WriteLine("Choose available action");

            while (true)
            {
                try
                {
                    var choice = int.Parse(Console.ReadLine());
                    if (actions[choice].IsAvailable)
                        return choice;
                }
                catch (Exception)
                {
                    Console.WriteLine("Expected to see integer");
                }
                Console.WriteLine("You've typed (UNAVAILABLE) action number, Retry!");
            }
        }

        public void InitPlayer()
        {
            var playerName = GetParameters("Enter Your name!");
            var formation = GetParameters($"Hey, {playerName}, enter your formation, within next format: X-X-X");
            var teamName = GetParameters($"Hey, {playerName}, enter your team name");

            while (true)
            {
                try
                {
                    game.AddPlayer(playerName, formation, teamName);
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    formation = GetParameters("Enter your formation, within next format: X-X-X");
                }
            }
            Console.WriteLine();
        }

        private string GetParameters(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        private IParameters GetParameters(IAction action, string strParameters)
        {
            var parametersType = action.GetType().BaseType.GetGenericArguments().First();
            var parserType = FindParser(parametersType);
            var parser = parserType.GetConstructor(new[] { typeof(Game) }).Invoke(new[] { game });
            return (IParameters)parserType.GetMethod("Parse").Invoke(parser, new[] { strParameters });
        }

        private Type FindParser(Type expectedTypeParameter)
            => GetParserTypes().Where(t => t.BaseType.GetGenericArguments().First() == expectedTypeParameter).First();

        private IEnumerable<Type> GetParserTypes()
        {
            return Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                       t.BaseType.GetGenericTypeDefinition() == typeof(ConsoleParser<>));
        }
    }
}
