using System.Linq;
using Scorewarrior.Test.Data;
using UnityEngine;
using Zenject;

namespace Scorewarrior.Test.Models
{
    public class WeaponModificationFactory : ModificationFactory<WeaponStats>
    {
        private Configuration _configuration;
        
        [Inject]
        private void Construct(Configuration configuration)
        {
            _configuration = configuration;
        }

        public override IStatsProvider<WeaponStats> GedModifiedStats(IStatsProvider<WeaponStats> stats)
        {
            ModifiedStats<WeaponStats> finalModificator = new WeaponModifiedStatsModifier(stats);
            var available = _configuration.WeaponModifications.ToList();
            int modificationCount = _configuration.WeaponModificationMaxCount;
            
            while (modificationCount > 0 && available.Count > 0)
            {
                int index = Random.Range(0, available.Count);
                ModificationStats<WeaponStats> modificator = available[index];
                
                finalModificator.Add(available[index]);
                available.RemoveAt(index);

                Debug.Log($"Модификатор оружия: {modificator.Id}");
                
                modificationCount--;
            }

            return finalModificator.GetStats();
        }
    }
}