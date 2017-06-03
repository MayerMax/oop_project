using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class BonusAction : Action
    {
        private BonusHolder bonusOwner;
        private BonusParameters parameters;

        public BonusAction(BonusHolder bonusOwner)
        {
            this.bonusOwner = bonusOwner;
        }

        public override bool SetSuitable(IParameters parameters)
        {
            this.parameters = SetParameters<BonusParameters>(parameters);
            return true;
        }

        public override bool Execute()
        {
            bonusOwner.RemoveUsedBonus(parameters.Bonus);
            parameters.Bonus.Apply(parameters.Card);
            return true;
        }

        public override string Explanation => "Apply a bonus to a given card";

        public override bool IsAvailable => bonusOwner.HasBonuses;
    }
}
