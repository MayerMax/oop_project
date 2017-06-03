using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class EnemyParameters : IParameters
    {
        public readonly Team Enemy;

        public EnemyParameters(Team enemy)
        {
            Enemy = enemy;
        }
    }
}
