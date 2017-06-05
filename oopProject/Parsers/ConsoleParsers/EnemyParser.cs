using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class EnemyParser : ConsoleParser<EnemyParameters>
    {
        public EnemyParser(Game game) : base(game) { }

        public override EnemyParameters Parse(string parameters)
            => new EnemyParameters(game.GetOpponents.First().Team);
    }
}
