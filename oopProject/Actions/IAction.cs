using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public interface IAction
    {
        string Explanation { get; }
        bool IsAvailable { get; }
        bool Execute(IParameters parameters);
        void SetUp(Game game);
        void Accept(ISuccess success);
    }
}
