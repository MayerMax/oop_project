using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class SwapCard : IAction
    {
        private Team team;
        public SwapCard(Team team) {
            this.team = team;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public string Explanation()
        {
            throw new NotImplementedException();
        }

        public bool IsAvailable()
        {
            throw new NotImplementedException();
        }
    }
}
