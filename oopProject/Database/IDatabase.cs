using System.Collections.Generic;

namespace oopProject
{
    public interface IDatabase
    { 
        PlayerInfo GetPlayerInfo(string name);
        PlayerInfo GetPlayerOfType(params string[] types);
        IEnumerable<PlayerInfo> GetPlayers(int count);
    }
}
