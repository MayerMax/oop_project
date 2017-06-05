using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class GetFromDeckParameters :IParameters
    {
        public readonly Deck Deck;

        public GetFromDeckParameters(Deck gameDeck) {
            Deck = gameDeck;
        }
    }
}
