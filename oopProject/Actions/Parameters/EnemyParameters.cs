namespace oopProject
{
    public class EnemyParameters : IParameters
    {
        public readonly Team Enemy;

        public EnemyParameters(Team enemy)
        {
            Enemy = enemy;
        }
    }
}
