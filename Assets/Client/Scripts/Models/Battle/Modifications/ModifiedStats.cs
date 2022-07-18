using System.Collections.Generic;
using Scorewarrior.Test.Data;

namespace Scorewarrior.Test.Models
{
    public abstract class ModifiedStats<T> where T : IStatsProvider<T>
    {
        protected readonly IStatsProvider<T> _wrapped;
        protected readonly List<ModificationStats<T>> _modifications = new();

        private IStatsProvider<T> _modified;

        protected ModifiedStats(IStatsProvider<T> wrapped)
        {
            _wrapped = _modified = wrapped;
        }

        public void Add(ModificationStats<T> modifier)
        {
            _modifications.Add(modifier);

            _modified = GetStatsInternal();
        }

        public void Remove(ModificationStats<T> modifier)
        {
            _modifications.Remove(modifier);

            _modified = GetStatsInternal(); 
        }

        public IStatsProvider<T> GetStats()
        {
            return _modified;
        }

        protected abstract IStatsProvider<T> GetStatsInternal();

        protected IModifier GetModifier(ModificationStats<T>.ModifierType modifierType)
        {
            return modifierType switch
            {
                ModificationStats<T>.ModifierType.Add => new StatsAddModifier(),
                ModificationStats<T>.ModifierType.Multiply => new StatsMultiplyModifier(),
                ModificationStats<T>.ModifierType.PercentAdd => new StatsPercentAddModifier(),
                _ => new StatsNoneModifier()
            };
        }
    }
}