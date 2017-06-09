using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        private Tuple<IAction, IParameters> GetActionAndParameters(int playerChoice)
        {
            var action = GetAvailableActions().ElementAt(playerChoice);
            var parser = GetParser(action);
            var parametersFormat = (string)parser.GetType()
                                                 .GetMethod("GetParametersFormat")
                                                 .Invoke(parser, new object[] { });
            while (true)
            {
                var strParameters = parametersFormat;
                try
                {
                    if (strParameters != "")
                        strParameters = GetUserInput(parametersFormat);
                    return Tuple.Create(action, GetParameters(parser, strParameters));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"{e.Message}. Retry!");
                }
            }
        }

        private void Turn() {
            Console.WriteLine("--------------------");
            PrintGameInfo();
            ShowAvailableActions();
            int playerChoice = PlayerChoice();
            while (true)
            {
                try
                {
                    game.Turn(GetActionAndParameters(playerChoice));
                    break;
                }
                catch (ArgumentException e) { Console.WriteLine($"{e.Message}. Retry!"); }
            }
            Console.WriteLine(game.Message);
            Console.WriteLine("--------------------\n");

        }

        public void Loop() {
            Console.WriteLine("The game is On!");
            while (!game.IsEnd) {
                try
                {
                    Turn();
                }
                catch (GameEndException) {
                    break;
                }
            }
            Console.WriteLine("The game is over!");
            Console.WriteLine($"{game.Score}\nThe winner is {game.Winner}");
        }

        private void ShowAvailableActions()
        {
            var i = 1;
            foreach (var action in GetAvailableActions())
                Console.WriteLine($"{i++}.{action.Explanation}");
        }

        private int PlayerChoice()
        {
            Console.WriteLine("Choose available action");
            var availableActionsAmount = GetAvailableActions().Count();
            while (true)
            {
                try
                {
                    var choice = int.Parse(Console.ReadLine());
                    if (choice > 0 && choice <= availableActionsAmount)
                        return --choice;
                }
                catch (Exception)
                {
                    Console.WriteLine("Expected to see integer");
                }
                Console.WriteLine("Invalid input, Retry!");
            }
        }

        private void InitPlayer()
        {
            var playerName = GetUserInput("Enter Your name!");
            var formationInfo = $"maximum {Squad.SQUAD_SIZE} players in total, zone limit - {Squad.ZONE_LIMIT} players";
            var formation = 
                GetUserInput($"Hey, {playerName}, enter your formation, within next format: X-X-X ({formationInfo})");
            var teamName = GetUserInput($"Hey, {playerName}, enter your team name");

            while (true)
            {
                try
                {
                    game.AddPlayer(playerName, formation, teamName);
                    break;
                }
                catch (Exception e)
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
        {
            try
            {
                return (IParameters)parser.GetType().GetMethod("Parse").Invoke(parser, new[] { strParameters });
            }
            catch (TargetInvocationException e) { throw e.InnerException; }
        }

        private object GetParser(IAction action)
        {
            var parametersType = action.GetType().BaseType.GetGenericArguments().First();
            var parserType = FindParser(parametersType);
            return parserType.GetConstructor(new[] { typeof(Game) }).Invoke(new[] { game });
        }

        private Type FindParser(Type expectedTypeParameter)
            => GetParserTypes().First(t => t.BaseType.GetGenericArguments().First() == expectedTypeParameter);

        private IEnumerable<Type> GetParserTypes()
        {
            return Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                       t.BaseType.GetGenericTypeDefinition() == typeof(ConsoleParser<>));
        }

        private void PrintGameInfo()
        {
            Console.WriteLine(game.Score);
            Console.WriteLine($"Moves left: {game.MovesLeft}\n");
            Console.WriteLine($"{game.CurrentPlayer.Name}'s turn\n");
            Console.WriteLine($"{game.CurrentPlayer.PrintTeam(game.BallPlace)}");
            Console.WriteLine("Opponent teams");
            foreach (var enemy in game.GetOpponents)
                Console.WriteLine($"{enemy.PrintTeam(game.BallPlace)}\n");
            Console.WriteLine($"Your Hand: {game.CurrentPlayer.Team.Hand.Print()}\n");
        }
    }
}
