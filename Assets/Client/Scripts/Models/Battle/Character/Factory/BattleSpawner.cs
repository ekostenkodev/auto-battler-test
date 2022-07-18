using System.Collections.Generic;
using Scorewarrior.Test.Models;
using UnityEngine;
using Zenject;

namespace Scorewarrior.Test
{
    public interface IBattleSpawner
    {
        void Spawn();
    }

    public class BattleSpawner : IBattleSpawner
    {
        private ICharacterFactory _factory;
        private SceneData _sceneData;
        private Configuration _configuration;

        [Inject]
        private void Construct(ICharacterFactory factory, SceneData sceneData, Configuration configuration)
        {
            _factory = factory;
            _sceneData = sceneData;
            _configuration = configuration;
        }

        public void Spawn()
        {
            List<CharacterConfiguration> availablePrefabs = new List<CharacterConfiguration>(_configuration.Characters);

            foreach (KeyValuePair<uint, List<Vector3>> positionsPair in GetSpawnPoints())
            {
                List<Vector3> positions = positionsPair.Value;
                int i = 0;
                while (i < positions.Count && availablePrefabs.Count > 0)
                {
                    int index = Random.Range(0, availablePrefabs.Count);
                    CharacterProvider character = _factory.Create(availablePrefabs[index], positionsPair.Key);

                    character.View.Root.position = positions[i];

                    availablePrefabs.RemoveAt(index);
                    i++;
                }
            }
        }

        private Dictionary<uint, List<Vector3>> GetSpawnPoints()
        {
            Dictionary<uint, List<Vector3>> spawnPositionsByTeam = new Dictionary<uint, List<Vector3>>();

            foreach (SpawnPointView spawn in _sceneData.Spawns)
            {
                if (false == spawnPositionsByTeam.ContainsKey(spawn.Team))
                {
                    spawnPositionsByTeam[spawn.Team] = new List<Vector3>();
                }

                spawnPositionsByTeam[spawn.Team].Add(spawn.transform.position);
            }

            return spawnPositionsByTeam;
        }
    }
}