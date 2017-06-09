using System.Linq;

namespace oopProject
{
    public class EnemyParser : ConsoleParser<EnemyParameters>
    {
        public EnemyParser(Game game) : base(game) { }

        public override EnemyParameters Parse(string parameters)
            => new EnemyParameters(Game.GetOpponents.First().Team);
    }
}
