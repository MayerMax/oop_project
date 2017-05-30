using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class PassAction : IAction
    {
        public string Explanation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsAvailable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool AreSuitable(IParameters parameters)
        {
            throw new NotImplementedException();
        }

        public bool Execute(IParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
