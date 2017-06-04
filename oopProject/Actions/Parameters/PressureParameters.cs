using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class PressureParameters : IParameters
    {
        public readonly Zone Zone;
        public readonly Team Enemy;

        public PressureParameters(Zone zone, Team enemy)
        {
            Zone = zone;
            Enemy = enemy;
        }
    }
}
