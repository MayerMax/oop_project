using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject.Actions
{
    interface BaseAction
    {
        bool IsAvailable();

        void Execute();

        string Explanation();


    }
}
