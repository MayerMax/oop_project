using System;
using System.Collections.Generic;
using System.Linq;

namespace oopProject
{
    public abstract class ConsoleParser<T>
        where T : class, IParameters
    {
        protected Game Game;
        private static HashSet<Type> emptyInputTypes = new HashSet<Type>{typeof(Deck), typeof(Team)};

        protected ConsoleParser(Game game)
        {
            this.Game = game;

        }

        public abstract T Parse(string parameters);

        public string GetParametersFormat() {
            var fieldsString = typeof(T).GetFields().Where(f => f.IsInitOnly && 
                                                           !emptyInputTypes.Contains(f.FieldType))
                                  .Select(f => $"{f.Name}({f.FieldType.Name})");
            return $"{string.Join(",", fieldsString)}";   
        }

        public int[] VerifyParameters(int expectedAmount, string[] splitted)
        {
            if (expectedAmount != splitted.Length)
                throw new ArgumentException($"Expected {expectedAmount} parameters, got {splitted.Length}");

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

        protected ZoneType VerifyZoneType(string strZoneType)
        {
            try
            {
                var type = (ZoneType)Enum.Parse(typeof(ZoneType), strZoneType);
                if (!Enum.IsDefined(typeof(ZoneType), type))
                    throw new InvalidCastException();
                return type;
            }
            catch (Exception)
            {
                var zoneTypes = Enum.GetValues(typeof(ZoneType))
                                    .OfType<ZoneType>()
                                    .Where(t => t != 0).ToArray();
                throw new ArgumentException(
                    $"Expected one of {string.Join(", ", zoneTypes)}, got {strZoneType}");
            }
        }

        protected IEnumerable<ZoneType> VerifyZoneTypes(string[] parameters, int[] indexes)
        {
            if (parameters.Length <= indexes.Last())
                throw new ArgumentException("Invalid parameters");
            foreach (var ind in indexes)
                yield return VerifyZoneType(parameters[ind]);
        }
    }

    public class ParseException : Exception
    {
    }
}
