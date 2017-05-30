using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    interface IAction
    {
        bool IsAvailable { get; }
        string Explanation { get; }


        bool AreSuitable(IParameters parameters);
        bool Execute(IParameters parameters);


    }
}
