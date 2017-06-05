using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    abstract class ConsoleParser<T> where T : class, IParameters
    {
        protected Game game;

        public ConsoleParser(Game game)
        {
            this.game = game;
        }

        public abstract T Parse(string parameters);

        public int[] VerifyParameters(int expectedAmount, string parameters)
        {
            var splitted = parameters.Split(' ');
            if (expectedAmount != splitted.Length)
                throw new ArgumentException($"Expected {expectedAmount} parameters, got {splitted.Length}");

            var constructorParameters = typeof(T).GetConstructors().First().GetParameters();
            var parsedParameters = new int[splitted.Length];
            for (var i = 0; i < splitted.Length; i++)
            {
                try
                {
                    parsedParameters[i] = int.Parse(splitted[i]);
                }
                catch (Exception)
                {
                    throw new ArgumentException($"Unexpected parameter {splitted[i]}");
                }
            }
            return parsedParameters;
        }
    }

    public class ParseException : Exception
    {
    }
}
