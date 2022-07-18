using System.Collections.Generic;
using System.Linq;
using Scorewarrior.Test.Data;
using UnityEngine;
using Zenject;

namespace Scorewarrior.Test.Models
{
    public class CharacterModificationFactory : ModificationFactory<CharacterStats>
    {
        private Configuration _configuration;
        
        [Inject]
        private void Construct(Configuration configuration)
        {
            _configuration = configuration;
        }

        public override IStatsProvider<CharacterStats> GedModifiedStats(IStatsProvider<CharacterStats> stats)
        {
            ModifiedStats<CharacterStats> finalModificator = new CharacterModifiedStatsModifier(stats);
            List<ModificationStats<CharacterStats>> available = _configuration.CharacterModifications.ToList();
            int modificationCount = _configuration.CharacterModificationMaxCount;
            
            while (modificationCount > 0 && available.Count > 0)
            {
                int index = Random.Range(0, available.Count);
                ModificationStats<CharacterStats> modificator = available[index];
                finalModificator.Add(available[index]);
                available.RemoveAt(index);
                
                Debug.Log($"Модификатор персонажа: {modificator.Id}");
                
                modificationCount--;
            }

            return finalModificator.GetStats();
        }
    }
}