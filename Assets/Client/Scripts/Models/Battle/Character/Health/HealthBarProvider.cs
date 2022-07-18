using System;
using Scorewarrior.Test.Data;

namespace Scorewarrior.Test.Models
{
    public class HealthBarProvider : IDisposable
    {
        private readonly HealthComponent _health;
        private readonly IStatsProvider<CharacterStats> _stats;
        
        public HealthBarView View { get; }

        public HealthBarProvider(
            HealthComponent health,
            IStatsProvider<CharacterStats> stats,
            HealthBarView view
        )
        {
            _health = health;
            _stats = stats;
            View = view;
            
            health.Changed += OnHealthChange;

            OnHealthChange();
        }

        private void OnHealthChange()
        {
            View.Health.Slider.value = _health.Health / _stats.GetStats().MaxHealth;
            View.Health.Text.text = $"{_health.Health}/{_stats.GetStats().MaxHealth}";
            
            View.Armor.Slider.value = _health.Armor / _stats.GetStats().MaxArmor;
            View.Armor.Text.text = $"{_health.Armor}/{_stats.GetStats().MaxArmor}";
        }

        public void Dispose()
        {
            _health.Changed -= OnHealthChange;
        }
    }
}