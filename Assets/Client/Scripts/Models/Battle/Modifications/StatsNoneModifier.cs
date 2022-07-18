namespace Scorewarrior.Test.Models
{
    public class StatsNoneModifier : IModifier
    {
        public float Modify(float value, float modifier)
        {
            return value;
        }

        public uint Modify(uint value, float modifier)
        {
            return value;
        }
    }
}