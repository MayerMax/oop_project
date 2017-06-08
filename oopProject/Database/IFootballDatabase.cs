using System.Collections.Generic;

namespace oopProject
{
    public interface IFootballDatabase
    {
        FootballCard GetCardOfType(ZoneType zone);
        IEnumerable<FootballCard> GetCards(int count);
    }
}
