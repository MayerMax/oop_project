namespace oopProject
{   
    // this class describes container for a squad line 
    public abstract class BaseZone
    {
        public readonly int Size;
        public readonly ZoneType Type;
        public readonly string ZoneName;

        protected BaseZone(int size, ZoneType type)
        {
            Type = type;
            Size = size;
            ZoneName = type.ToString();
        }

    }
}
