using System;
using System.Collections.Generic;
using System.Linq;

namespace oopProject
{
    interface IDatabase
    { 
        PlayerInfo GetPlayerInfo(string name);
        PlayerInfo GetPlayerOfType(params string[] types);
        IEnumerable<PlayerInfo> GetPlayers(int count);
        //IEnumerable<PlayerInfo> WithEqualAttribute(string attribute);
    }
}
