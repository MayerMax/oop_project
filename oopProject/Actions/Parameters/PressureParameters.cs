namespace oopProject
{
    public class PressureParameters : IParameters
    {
        public readonly ZoneType ZoneType;
        public readonly Team Enemy;

        public PressureParameters(ZoneType zoneType, Team enemy)
        {
            ZoneType = zoneType;
            Enemy = enemy;
        }
    }
}
