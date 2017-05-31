using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class BonusHolder
    {
        private Dictionary<Bonus, int> bonusesCount;

        public BonusHolder() => bonusesCount = new Dictionary<Bonus, int>();

        public bool HasBonuses => bonusesCount.Any();

        public void ReceiveBonus(Bonus bonus)
        {
            if (bonusesCount.ContainsKey(bonus))
                bonusesCount[bonus]++;
            else
                bonusesCount[bonus] = 1;
        }

        public void RemoveUsedBonus(Bonus bonus)
        {
            if (!bonusesCount.ContainsKey(bonus))
                throw new InvalidOperationException(
                    string.Format("Can't remove {0}, it hasn't been received yet",
                                  bonus.Name));
            bonusesCount[bonus]--;
        }
    }
}
