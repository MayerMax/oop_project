using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    public class GetFromDeckAction : Action
    {
        private GetFromDeckParameters parameters;

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
            game.CurrentPlayer.Team.Hand.InsertCard(deck.GetCard());
            wasSuccessfullyExecuted = true;
            return wasSuccessfullyExecuted;
        }

        public override void Accept(ISuccess success) => success.Apply(this, wasSuccessfullyExecuted);
    }
}
