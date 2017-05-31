using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class BonusAction : IAction
    {
        private BonusHolder bonusOwner;

        public BonusAction(BonusHolder bonusOwner)
        {
            this.bonusOwner = bonusOwner;
        }

        public bool AreSuitable(IParameters parameters)
        {
            throw new NotImplementedException();
        }

        bool IAction.Execute(IParameters parameters)
        {
            var bonusParameters = parameters as BonusParameters;
            bonusOwner.RemoveUsedBonus(bonusParameters.Bonus);
            bonusParameters.Bonus.Apply(bonusParameters.Card);
            return true;
        }

        public string Explanation => "Apply a bonus to a given card";

        public bool IsAvailable => bonusOwner.HasBonuses;
    }
}
