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


        public Zone(int size, ZoneType type, List<FootballCard> cards, bool hasBall = false) : base(size, type)
        {
            HasBall = hasBall;
            line = new Line(cards, type, HasBall);

        }


        public static Zone Create(int size, ZoneType type, List<FootballCard> cards) {

            return new Zone(size, type, cards);
        }

    }
}
