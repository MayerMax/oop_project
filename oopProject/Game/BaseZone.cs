using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{   
    // this class describes container for a squad line 
    public abstract class BaseZone
    {
        public readonly int Size;
        public readonly ZoneType Type;
        public readonly string ZoneName;

        public BaseZone(int size, ZoneType type)
        {
            Type = type;
            Size = size;
            ZoneName = type.ToString();
        }

    }
}
