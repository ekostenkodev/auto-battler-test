namespace Scorewarrior.Test.Models
{
    public class StatsAddModifier : IModifier
    {
        public float Modify(float value, float modifier)
        {
            return value + modifier;
        }

        public uint Modify(uint value, float modifier)
        {
            return (uint)(value + modifier);
        }
    }
}