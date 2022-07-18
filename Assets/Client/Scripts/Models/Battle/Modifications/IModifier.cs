namespace Scorewarrior.Test.Models
{
    public interface IModifier
    {
        float Modify(float value, float modifier);
        uint Modify(uint value, float modifier);
    }
}