﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace oopProject
{
    public abstract class ConsoleParser<T>
        where T : class, IParameters
    {
        protected Game Game;

        protected ConsoleParser(Game game)
        {
            this.Game = game;

        }

        public abstract T Parse(string parameters);

        public string GetParametersFormat() {
            var fieldsString = typeof(T).GetFields().Where(f => f.IsInitOnly)
                                  .Select(f => $"{f.Name}({f.FieldType.Name})");
            return $"{string.Join(",", fieldsString)}";
            
            
        }

        public int[] VerifyParameters(int expectedAmount, string[] splitted)
        {
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

        protected ZoneType VerifyZoneType(string strZoneType)
        {
            try
            {
                return (ZoneType)Enum.Parse(typeof(ZoneType), strZoneType);
            }
            catch (ArgumentException)
            {
                var zoneTypes = Enum.GetValues(typeof(ZoneType))
                                    .OfType<ZoneType>()
                                    .Where(t => t != 0).ToArray();
                throw new ArgumentException(
                    $"Expected one of {string.Join(", ", zoneTypes)}, got {strZoneType}");
            }
        }

        protected IEnumerable<ZoneType> VerifyZoneTypes(string[] strZoneTypes)
            => strZoneTypes.Select(VerifyZoneType);
    }

    public class ParseException : Exception
    {
    }
}
