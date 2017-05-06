using System;
using System.Collections.Generic;
using System.Linq;

namespace oopProject
{
    interface IDatabase
    {
        PlayerInfo GetPlayerInfo(string name);
        IEnumerable<PlayerInfo> WithEqualAttribute(string attribute);
    }
}
