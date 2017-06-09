namespace oopProject
{
    public class SwapAction : Action<SwapParameters>
    {
        public override bool Execute(SwapParameters parameters)
        {
            Game.CurrentPlayer.Team.SubstitutionFromHandToSquad(parameters.OldCardZone,
                parameters.CardPosition, parameters.NewCardPosition);
            WasSuccessfullyExecuted = true;
            return WasSuccessfullyExecuted;
        }

        public override bool AreSuitable(SwapParameters parameters)
        {
            var team = Game.CurrentPlayer.Team;
            var isActive = team.Squad.IsActive(parameters.OldCardZone, parameters.CardPosition);
            return isActive && team.Hand.Contains(parameters.NewCardPosition);
        }

        public override string Explanation => "Swap cards";

        public override int Value => 5;

        public override bool IsAvailable => Game.CurrentPlayer.Team.Hand.Any && 
                                            Game.CurrentPlayer.Team.Squad.Any;

        public override void Accept(ISuccess success) => success.Apply(this, WasSuccessfullyExecuted);
    }
}
