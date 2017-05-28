using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    abstract class Bonus
    {
        public abstract void Apply(FootballCard card);
        public abstract string Name { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var bonus = (Bonus)obj;
            return Name == bonus.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
