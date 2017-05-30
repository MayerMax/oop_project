using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class BonusAction : IAction
    {
        private BonusHolder bonusHolder;

        public BonusAction(BonusHolder bonusOwner)
        {
            this.bonusHolder = bonusOwner;
        }

        public void Execute(IParameters parameters)
        {
            var bonusParameters = parameters as BonusParameters;
            bonusHolder.RemoveUsedBonus(bonusParameters.Bonus);
            bonusParameters.Bonus.Apply(bonusParameters.Card);
        }

        public string Explanation => "Apply a bonus to a given card";

        public bool IsAvailable => bonusHolder.HasBonuses;
    }
}
