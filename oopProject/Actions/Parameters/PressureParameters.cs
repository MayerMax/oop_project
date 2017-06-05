using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class PressureParameters : IParameters
    {
        public readonly ZoneType ZoneType;
        public readonly Team Enemy;

        public PressureParameters(ZoneType zoneType, Team enemy)
        {
            ZoneType = zoneType;
            Enemy = enemy;
        }
    }
}
