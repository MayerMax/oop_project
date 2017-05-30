using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Ball
    {
        private static Dictionary<ZoneType, ZoneType> transitions =
            new Dictionary<ZoneType, ZoneType>() {
                { ZoneType.DEF, ZoneType.ATT },
                { ZoneType.MID, ZoneType.MID },
                { ZoneType.ATT, ZoneType.DEF }
            };

        public Player Owner { get; private set; }
        public ZoneType BallPlace { get; private set; }
        private List<Player> observers;

        public Ball() {
            observers = new List<Player>();
        }

        public void AddObserver(Player observer) {
            if (observers.Count == 0) {
                Owner = observer;
                BallPlace = ZoneType.MID;
            }
            observers.Add(observer);

        }
        public void Move() {
            if (BallPlace < ZoneType.ATT)
                BallPlace = (ZoneType)((int)BallPlace + 1);
        }

        public void Intercept(Player newOwner) {
            Owner = observers[observers.IndexOf(newOwner)];
            BallPlace = transitions[BallPlace];
        }
        
    }

    
    }

