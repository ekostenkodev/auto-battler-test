using Scorewarrior.Test.Data;

namespace Scorewarrior.Test.Models
{
    public abstract class ModificationFactory<T> where T : IStatsProvider<T>
    {
        public abstract IStatsProvider<T> GedModifiedStats(IStatsProvider<T> stats);
    }
}