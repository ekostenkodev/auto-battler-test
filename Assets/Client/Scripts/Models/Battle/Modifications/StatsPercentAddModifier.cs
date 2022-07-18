namespace Scorewarrior.Test.Models
{
    public class StatsPercentAddModifier : IModifier
    {
        public float Modify(float value, float modifier)
        {
            return value + (value * modifier);
        }

        public uint Modify(uint value, float modifier)
        {
            return (uint)(value + (value * modifier));

        }
    }
}