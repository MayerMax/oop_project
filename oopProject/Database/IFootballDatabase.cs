using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public interface IFootballDatabase
    {
        FootballCard GetCardOfType(ZoneType zone);
        IEnumerable<FootballCard> GetCards(int count);
    }
}
