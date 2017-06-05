using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class PressureParser : ConsoleParser<PressureParameters>
    {
        public PressureParser(Game game) : base(game) { }

        public override PressureParameters Parse(string parameters)
        {
            var verified = VerifyParameters(1, parameters);
            return new PressureParameters((ZoneType)verified[0], game.GetOpponents.First().Team);
        }
    }
}
