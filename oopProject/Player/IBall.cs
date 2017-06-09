using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public interface IBall
    {
        ZoneType Place { get; }
        string Owner { get; }

        void AddObserver(Team observer);
        void Move();
        void InterceptedBy(Team newOwner);

        void Restart(Team newOwner);
        bool IsOwner(Team team);

    }
}
