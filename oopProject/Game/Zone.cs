using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Zone : BaseZone
    {
        public bool HasBall { get; private set; } // TODO
        private Line line;
        public int ZonePower { get { return line.ZonePower(); } }


        public Zone(ZoneType type, List<FootballCard> cards, bool hasBall = false) : base(cards.Count, type)
        {
            HasBall = hasBall;
            line = new Line(cards, type, HasBall);

        }

        public void RemoveCard(FootballCard card) {
            line.Remove(card);
        }

        public void InsertCard(FootballCard card) {
            line.Insert(card);
        }
    }
}
