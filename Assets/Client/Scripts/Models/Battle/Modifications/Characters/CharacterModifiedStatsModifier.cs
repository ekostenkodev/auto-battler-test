using Scorewarrior.Test.Data;

namespace Scorewarrior.Test.Models
{
    public class CharacterModifiedStatsModifier : ModifiedStats<CharacterStats>
    {
        public CharacterModifiedStatsModifier(IStatsProvider<CharacterStats> wrapped) : base(wrapped)
        {
        }

        protected override IStatsProvider<CharacterStats> GetStatsInternal()
        {
            IStatsProvider<CharacterStats> newStats = _wrapped;
            
            foreach (ModificationStats<CharacterStats> modificator in _modifications)
            {
                var modifier = GetModifier(modificator.Type);

                newStats = new CharacterStats
                (
                    modifier.Modify(newStats.GetStats().Accuracy, modificator.Modificator.Accuracy),
                    modifier.Modify(newStats.GetStats().Dexterity, modificator.Modificator.Dexterity),
                    modifier.Modify(newStats.GetStats().MaxHealth, modificator.Modificator.MaxHealth),
                    modifier.Modify(newStats.GetStats().MaxArmor, modificator.Modificator.MaxArmor),
                    modifier.Modify(newStats.GetStats().AimTime, modificator.Modificator.AimTime)
                );
            }

            return newStats;
        }
    }
}