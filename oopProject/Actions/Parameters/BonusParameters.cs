using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class BonusParameters : IParameters
    {
        public readonly Bonus Bonus;
        public readonly FootballCard Card;

        public BonusParameters(Bonus bonus, FootballCard card)
        {
            Bonus = bonus;
            Card = card;
        }
    }
}
