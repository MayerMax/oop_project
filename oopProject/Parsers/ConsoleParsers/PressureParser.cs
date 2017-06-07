using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class PressureParser : ConsoleParser<PressureParameters>
    {
        public PressureParser(Game game) : base(game) { }

        public override PressureParameters Parse(string parameters)
        {
            var splitted = parameters.Split(' ');
            var zoneType = VerifyZoneType(splitted[0]);
            splitted[0] = ((int)zoneType).ToString();
            var verified = VerifyParameters(1, splitted);
            return new PressureParameters(zoneType, game.GetOpponents.First().Team);
        }
    }
}
