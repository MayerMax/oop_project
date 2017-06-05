using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class SwapParser : ConsoleParser<SwapParameters>
    {
        public SwapParser(Game game) : base(game) { }

        public override SwapParameters Parse(string parameters)
        {
            var verified = VerifyParameters(3, parameters);
            return new SwapParameters(verified[0], (ZoneType)verified[1], verified[2]);
        }
    }
}
