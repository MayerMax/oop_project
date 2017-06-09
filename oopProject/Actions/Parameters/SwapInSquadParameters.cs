using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class SwapInSquadParameters : IParameters
    {
        public readonly ZoneType First;
        public readonly int FirstPos;
        public readonly ZoneType Second;
        public readonly int SecondPos;

        public SwapInSquadParameters(ZoneType first, int firstPos,
            ZoneType second, int secondPos)
        {
            First = first;
            FirstPos = firstPos;
            Second = second;
            SecondPos = secondPos;
        }
    }
}
