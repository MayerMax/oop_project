using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Ball
    {
        public static ReadOnlyDictionary<ZoneType, ZoneType> Transitions =
            new ReadOnlyDictionary < ZoneType, ZoneType>(
                new Dictionary<ZoneType, ZoneType>() {
                { ZoneType.DEF, ZoneType.ATT },
                { ZoneType.MID, ZoneType.MID },
                { ZoneType.ATT, ZoneType.DEF }});
        
        private List<Team> observers;
        private Team owner;
        public ZoneType BallPlace { get; private set; }

        public Ball() {
            observers = new List<Team>();
        }

        public void AddObserver(Team observer) {
            if (observers.Count == 0) {
                owner = observer;
                BallPlace = ZoneType.MID;
            }
            observers.Add(observer);
        }

        public void NotifyObserversExcept(Team team)
        {
            foreach (var observer in observers.Where(o => !o.Equals(team)))
                observer.Update(null);
        }

        public void Move() {
            if (BallPlace < ZoneType.ATT)
                BallPlace = (ZoneType)((int)BallPlace + 1);
        }

        public void Intercept(Team newOwner) {
            owner = observers[observers.IndexOf(newOwner)];
            BallPlace = Transitions[BallPlace];
            owner.Update(this);
            NotifyObserversExcept(owner);
        }

        public bool IsOwner(Team team) => owner.Equals(team);
    }
}

