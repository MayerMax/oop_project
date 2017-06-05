using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class DeckParser : ConsoleParser<GetFromDeckParameters>
    {
        public DeckParser(Game game) : base(game) { }

        public override GetFromDeckParameters Parse(string parameters)
            => new GetFromDeckParameters(game.Deck);
    }
}
