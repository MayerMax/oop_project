namespace oopProject
{
    public class GetFromDeckParameters :IParameters
    {
        public readonly Deck Deck;

        public GetFromDeckParameters(Deck gameDeck) {
            Deck = gameDeck;
        }
    }
}
