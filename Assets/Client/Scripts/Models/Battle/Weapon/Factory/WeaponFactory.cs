using System;
using Scorewarrior.Test.Data;
using Scorewarrior.Test.Views;
using Zenject;
using Object = UnityEngine.Object;

namespace Scorewarrior.Test.Models
{
    public class WeaponFactory : IWeaponFactory
    {
        private ModificationFactory<WeaponStats> _modificationFactory;
        
        public event Action<WeaponProvider> Created;

        [Inject]
        private void Construct(ModificationFactory<WeaponStats> modificationFactory)
        {
            _modificationFactory = modificationFactory;
        }

        public WeaponProvider Create(WeaponConfiguration configuration)
        {
            IStatsProvider<WeaponStats> stats = _modificationFactory.GedModifiedStats(configuration.Stats);
            WeaponModel model = new WeaponModel(stats);
            WeaponView view = Object.Instantiate(configuration.View);
            WeaponProvider provider = new WeaponProvider(model, view);
            
            Created?.Invoke(provider);

            return provider;
        }
    }
}