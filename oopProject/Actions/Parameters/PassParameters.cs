using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class PassParameters : IParameters
    {
        public readonly Player Enemy;

        public PassParameters(Player enemy)
        {
            Enemy = enemy;
        }
    }
}
