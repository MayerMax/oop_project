using System.Linq;

namespace oopProject
{
    public class PressureParser : ConsoleParser<PressureParameters>
    {
        public PressureParser(Game game) : base(game) { }

        public override PressureParameters Parse(string parameters)
        {
            var splitted = parameters.Split(' ');
            var zoneType = VerifyZoneType(splitted[0]);
            return new PressureParameters(zoneType, Game.GetOpponents.First().Team);
        }
    }
}
