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

        public PressureParameters(Zone zone)
        {
            Zone = zone;
        }
    }
}
