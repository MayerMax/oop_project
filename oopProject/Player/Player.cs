using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Player
    {
        public readonly string Name;
        public readonly string Formation;

        private Squad squad;
        private Hand hand;
        private Dictionary<Bonus, int> bonusesCount;
        public Team Team { get; set; }

        public Player(FootballDatabase db, string name, string formation) {
            if (!Squad.ValidateSquad(formation))
                throw new ArgumentException("Incorrect squad given!");
            Name = name;
            Formation = formation;
            var filledSquad = FillSquadWithRandom(formation, db);
            hand = new Hand(new List<FootballCard> { }); // Left it free for now
            squad = new Squad(name, db.GetCardOfType(ZoneType.GK), filledSquad);
            Team = new Team(squad, hand);
        }

        public bool HasBonus(Bonus bonus) => bonusesCount.ContainsKey(bonus);

        public void ReceiveBonus(Bonus bonus)
        {
            if (bonusesCount.ContainsKey(bonus))
                bonusesCount[bonus]++;
            else
                bonusesCount[bonus] = 1;
        }

        public void RemoveUsedBonus(Bonus bonus)
        {
            if (!bonusesCount.ContainsKey(bonus))
                throw new InvalidOperationException(
                    string.Format("{0} hasn't been received by the player yet", bonus.Name));
            bonusesCount[bonus]--;
        }

        private Dictionary<ZoneType, List<FootballCard>> FillSquadWithRandom(string formation, FootballDatabase db) {
            int[] eachzoneSize = formation.Split('-').Select(elem => int.Parse(elem)).ToArray();

            var resultFiller = new Dictionary<ZoneType, List<FootballCard>>();
            resultFiller.Add(ZoneType.DEF, db.GetCards(eachzoneSize[0]).ToList());
            resultFiller.Add(ZoneType.MID, db.GetCards(eachzoneSize[1]).ToList());
            resultFiller.Add(ZoneType.ATT, db.GetCards(eachzoneSize[2]).ToList());

           return resultFiller;
        }
    }
}
