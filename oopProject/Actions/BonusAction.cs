using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class BonusAction : IAction<BonusParameters>
    {
        private Player player;

        public BonusAction(Player player) {
            this.player = player;
        }

        public void Execute(BonusParameters parameters)
        {
            player.RemoveUsedBonus(parameters.Bonus);
            parameters.Bonus.Apply(parameters.Card);
        }

        public string Explanation => "Apply a bonus to a given card";

        public bool IsAvailable => player.HasBonuses;
    }
}
