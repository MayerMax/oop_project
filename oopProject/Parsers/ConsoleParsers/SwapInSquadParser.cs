using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class SwapInSquadParser : ConsoleParser<SwapInSquadParameters>
    {
        public SwapInSquadParser(Game game) : base(game) { }

        public override SwapInSquadParameters Parse(string parameters)
        {
            var splitted = parameters.Split(' ');
            var zoneTypes = VerifyZoneTypes(new[] {splitted[0], splitted[2]}).ToArray();
            splitted[0] = ((int)zoneTypes[0]).ToString();
            splitted[2] = ((int)zoneTypes[1]).ToString();
            var verified = VerifyParameters(4, splitted);
            return new SwapInSquadParameters(zoneTypes[0], --verified[1], zoneTypes[1],  --verified[3]);
        }
    }
}
