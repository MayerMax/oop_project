namespace oopProject
{
    public class Position
    {
        public FootballCard Card { get; set; }
        public readonly int Number;
        public bool IsFree => Card == null;

        public Position(int place, FootballCard card) {
            Card = card;
            Number = place;
        }

        public override bool Equals(object obj)
        {
            var card = (FootballCard)obj;
            return card.CardName.Equals(Card.CardName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void Release() {
            Card.LeaveZone();
            Card = null;
        }
    }
}
