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
        private IEnumerable<IAction> GetAvailableActions()
            => actions.Where(a => a.IsAvailable);

        public ConsoleController(Game game, List<IAction> actions)
        {
            this.game = game;
            this.actions = actions;
            foreach (var action in this.actions)
                action.SetUp(this.game);
            for (int i = 0; i < Game.PLAYERS_AMOUNT; i++)
                InitPlayer();
        }

        private Tuple<IAction, IParameters> RequireChoice()
        {
            int playerChoice = PlayerChoice();
            var action = GetAvailableActions().ElementAt(playerChoice);
            var parser = GetParser(action);
            var parametersFormat = (string)parser.GetType()
                                                 .GetMethod("GetParametersFormat")
                                                 .Invoke(parser, new object[] { });
            var strParameters = GetUserInput(parametersFormat);
            return Tuple.Create(action, GetParameters(parser, strParameters));
        }

        private void Turn() {
            Console.WriteLine("--------------------");
            Console.WriteLine($"{game.CurrentPlayer.Name}'s turn");
            Console.WriteLine($"{game.CurrentPlayer.PrintTeam()}");
            ShowAvailableActions();
            game.Turn(RequireChoice());
            Console.WriteLine(game.Message);
            Console.WriteLine("--------------------\n");

        }

        public void Loop() {
            Console.WriteLine("The game is On!");
            while (true) {
                try
                {
                    Turn();
                }
                catch (GameEndException) {
                    Console.WriteLine("The game is over!");
                    /// TODO
                }
            }
        }
        private void ShowAvailableActions()
        {
            var i = 0;
            foreach (var action in GetAvailableActions())
                Console.WriteLine($"{i++}.{action.Explanation}");
        }

        private int PlayerChoice()
        {
            Console.WriteLine("Choose available action");

            while (true)
            {
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Expected to see integer");
                }
                //Console.WriteLine("You've typed (UNAVAILABLE) action number, Retry!");
            }
        }

        private void InitPlayer()
        {
            var playerName = GetUserInput("Enter Your name!");
            var formation = GetUserInput($"Hey, {playerName}, enter your formation, within next format: X-X-X");
            var teamName = GetUserInput($"Hey, {playerName}, enter your team name");

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
                    formation = GetUserInput("Enter your formation, within next format: X-X-X");
                }
            }
            Console.WriteLine();
        }

        private string GetUserInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        private IParameters GetParameters(object parser, string strParameters)
            => (IParameters)parser.GetType().GetMethod("Parse").Invoke(parser, new[] { strParameters });

        private object GetParser(IAction action)
        {
            var parametersType = action.GetType().BaseType.GetGenericArguments().First();
            var parserType = FindParser(parametersType);
            return parserType.GetConstructor(new[] { typeof(Game) }).Invoke(new[] { game });
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
