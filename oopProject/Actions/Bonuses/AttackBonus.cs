using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class AttackBonus : Bonus
    {
        public override void Apply(FootballCard card) => card.Attack++;

        public override string Name => "AttackBonus";
    }
}
