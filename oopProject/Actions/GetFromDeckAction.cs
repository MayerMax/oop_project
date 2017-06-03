using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class GetFromDeckAction : IAction
    {
        private Team team;

        public GetFromDeckAction(Team team) {
            this.team = team;
        }

        public string Explanation => "Get card from deck and put it in hand";

        public bool IsAvailable => true;

        public bool AreSuitable(IParameters parameters) => parameters is GetFromDeckParameters;

        public bool Execute(IParameters parameters) {
            var deckParams = parameters as GetFromDeckParameters;
            Deck deck = deckParams.Deck;
            if (!deck.Any)
                return false;
            team.Hand.InsertCard(deck.GetCard());
            return true;
        }

    }
}
