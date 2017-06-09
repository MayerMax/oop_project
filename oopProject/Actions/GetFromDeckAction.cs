namespace oopProject
{
    public class GetFromDeckAction : Action<GetFromDeckParameters>
    {
        public override string Explanation => "Get card from deck and put it in hand";

        public override int Value => 5;

        public override bool IsAvailable => true;

        public override bool AreSuitable(GetFromDeckParameters parameters) => true;

        public override bool Execute(GetFromDeckParameters parameters) {
            var deck = parameters.Deck;
            if (!deck.Any)
                return false;
            Game.CurrentPlayer.Team.Hand.InsertCard(deck.GetCard());
            WasSuccessfullyExecuted = true;
            return WasSuccessfullyExecuted;
        }

        public override void Accept(ISuccess success) => success.Apply(this, WasSuccessfullyExecuted);
    }
}
