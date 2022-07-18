namespace Scorewarrior.Test.Data
{
    public interface IStatsProvider<T>
    {
        T GetStats();
    }
}