using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    interface IAction<T> where T: IParameters
    {
        bool IsAvailable { get; }
        string Explanation { get; }
        void Execute(T parameters);
    }
}
