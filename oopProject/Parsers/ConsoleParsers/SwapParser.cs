
using System.Linq;

namespace oopProject
{
    public class SwapParser : ConsoleParser<SwapParameters>
    {
        public SwapParser(Game game) : base(game) { }
        
        public override SwapParameters Parse(string parameters)
        {
            var splitted = parameters.Split(' ');
            var zoneType = VerifyZoneTypes(splitted, new [] {1}).First();
            splitted[1] = ((int)zoneType).ToString();
            var verified = VerifyParameters(3, splitted);
            return new SwapParameters(--verified[0], zoneType, --verified[2]);
        }
    }
}
