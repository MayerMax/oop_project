namespace oopProject
{
    public class DeckParser : ConsoleParser<GetFromDeckParameters>
    {
        public DeckParser(Game game) : base(game) { }

        public override GetFromDeckParameters Parse(string parameters)
            => new GetFromDeckParameters(Game.Deck);
    }
}
