using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
     interface ISuccess {

         string Message { get;}
         void Apply(SwapAction action, bool isTrue);
         void Apply(PassAction action, bool isTrue);
        //TODO

    }

    class Success : ISuccess
    {
        public string Message
        {
            get
            {
                throw new NotImplementedException();
            }

            
        }

        public void Apply(PassAction action, bool isTrue)
        {
            throw new NotImplementedException();
        }

        public void Apply(SwapAction action, bool isTrue)
        {
            throw new NotImplementedException();
        }
    }
}
