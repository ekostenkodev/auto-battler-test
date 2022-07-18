using Scorewarrior.Test.Data;

namespace Scorewarrior.Test.Models
{
    public class WeaponModifiedStatsModifier : ModifiedStats<WeaponStats>
    {
        public WeaponModifiedStatsModifier(IStatsProvider<WeaponStats> wrapped) : base(wrapped)
        {
        }

        protected override IStatsProvider<WeaponStats> GetStatsInternal()
        {
            IStatsProvider<WeaponStats> newStats = _wrapped;
            
            foreach (ModificationStats<WeaponStats> modificator in _modifications)
            {
                var modifier = GetModifier(modificator.Type);

                newStats = new WeaponStats
                (
                    modifier.Modify(newStats.GetStats().Damage, modificator.Modificator.Damage),
                    modifier.Modify(newStats.GetStats().Accuracy, modificator.Modificator.Accuracy),
                    modifier.Modify(newStats.GetStats().FireRate, modificator.Modificator.FireRate),
                    modifier.Modify(newStats.GetStats().ClipSize, modificator.Modificator.ClipSize),
                    modifier.Modify(newStats.GetStats().ReloadTime, modificator.Modificator.ReloadTime),
                    modifier.Modify(newStats.GetStats().BulletSpeed, modificator.Modificator.BulletSpeed)
                    
                );
            }

            return newStats;
        }
    }
}