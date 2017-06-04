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

        public string Owner => owner.Squad.Name;

        public Ball(ZoneType whereToStart=ZoneType.NONE) {
            observers = new List<Team>();
            if (whereToStart != ZoneType.NONE)
                BallPlace = whereToStart;

        }

        public void AddObserver(Team observer) {
            if (observers.Count == 0) {
                owner = observer;
                if (BallPlace == ZoneType.NONE)
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

        public void InterceptedBy(Team newOwner) {
            UpdateObservers(newOwner);
            BallPlace = Transitions[BallPlace];
        }

        public void Restart(Team newOwner)
        {
            UpdateObservers(newOwner);
            BallPlace = ZoneType.MID;
        }

        private void UpdateObservers(Team newOwner)
        {
            owner = observers[observers.IndexOf(newOwner)];
            owner.Update(this);
            NotifyObserversExcept(owner);
        }

        public bool IsOwner(Team team) => owner.Equals(team);
    }
}

