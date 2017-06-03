using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class GetFromDeckAction : Action
    {
        private Team team;
        private GetFromDeckParameters parameters;

        public GetFromDeckAction(Team team) {
            this.team = team;
        }

        public override string Explanation => "Get card from deck and put it in hand";

        public override bool IsAvailable => true;

        public override bool SetSuitable(IParameters parameters) {
            this.parameters = SetParameters<GetFromDeckParameters>(parameters);
            return true;
        }

        public override bool Execute() {
            CheckParameters(parameters);

            Deck deck = parameters.Deck;
            if (!deck.Any)
                return false;
            team.Hand.InsertCard(deck.GetCard());
            return true;
        }

    }
}
