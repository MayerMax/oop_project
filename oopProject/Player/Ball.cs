using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace oopProject
{
    public class Ball
    {
        public static ReadOnlyDictionary<ZoneType, ZoneType> Transitions =
            new ReadOnlyDictionary < ZoneType, ZoneType>(
                new Dictionary<ZoneType, ZoneType>() {
                { ZoneType.DEF, ZoneType.ATT },
                { ZoneType.MID, ZoneType.MID },
                { ZoneType.ATT, ZoneType.DEF }});
        
        private List<Team> observers;
        private Team owner;
        public ZoneType Place { get; private set; }

        public string Owner => owner.Squad.Name;

        public Ball(ZoneType whereToStart=ZoneType.NONE) {
            observers = new List<Team>();
            if (whereToStart != ZoneType.NONE)
                Place = whereToStart;

        }

        public void AddObserver(Team observer) {
            if (observers.Count == 0) {
                owner = observer;
                if (Place == ZoneType.NONE)
                    Place = ZoneType.MID;
            }
            observers.Add(observer);
        }

        public void NotifyObserversExcept(Team team)
        {
            foreach (var observer in observers.Where(o => !o.Equals(team)))
                observer.Update(null);
        }

        public void Move() {
            if (Place < ZoneType.ATT)
                Place = (ZoneType)((int)Place + 1);
        }

        public void InterceptedBy(Team newOwner) {
            UpdateObservers(newOwner);
            Place = Transitions[Place];
        }

        public void Restart(Team newOwner)
        {
            UpdateObservers(newOwner);
            Place = ZoneType.MID;
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

