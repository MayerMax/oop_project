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
        
        private List<Player> observers;
        private Player owner;
        public ZoneType BallPlace { get; private set; }

        public Ball() {
            observers = new List<Player>();
        }

        public void AddObserver(Player observer) {
            if (observers.Count == 0) {
                owner = observer;
                BallPlace = ZoneType.MID;
            }
            observers.Add(observer);
        }

        public void NotifyObserversExcept(Player player)
        {
            foreach (var observer in observers.Where(o => !o.Equals(player)))
                observer.Update(null);
        }

        public void Move() {
            if (BallPlace < ZoneType.ATT)
                BallPlace = (ZoneType)((int)BallPlace + 1);
        }

        public void Intercept(Player newOwner) {
            owner = observers[observers.IndexOf(newOwner)];
            BallPlace = Transitions[BallPlace];
            owner.Update(this);
            NotifyObserversExcept(owner);
        }

        public bool IsOwner(Player player) => owner.Equals(player);
    }
}

