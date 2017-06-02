using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class PassParameters : IParameters
    {
        public readonly Team Enemy;

        public PassParameters(Team enemy)
        {
            Enemy = enemy;
        }
    }
}
