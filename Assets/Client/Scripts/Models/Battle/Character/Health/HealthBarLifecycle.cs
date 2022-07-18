using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Scorewarrior.Test.Models
{
    public class HealthBarLifecycle : IDisposable, ITickable
    {
        private readonly Vector3 HealthBarOffset = new(0, 5, 0);
        
        private readonly ObjectPool<HealthBarView> _pool = new();
        private readonly Dictionary<CharacterProvider, HealthBarProvider> _healthBars = new();

        private ICharacterFactory _characterFactory;
        private ICharacterLifecycle _characterLifecycle;
        private Configuration _configuration;
        private SceneData _sceneData;

        [Inject]
        private void Construct(
            ICharacterFactory characterFactory,
            ICharacterLifecycle characterLifecycle,
            Configuration configuration,
            SceneData sceneData
        )
        {
            _characterFactory = characterFactory;
            _characterLifecycle = characterLifecycle;
            _configuration = configuration;
            _sceneData = sceneData;

            _characterFactory.Created += CreateHealthBar;
            _characterLifecycle.Diad += DeleteHealthBar;
        }
        
        public void Tick()
        {
            foreach (KeyValuePair<CharacterProvider, HealthBarProvider> healthBar in _healthBars)
            {
                UpdatePosition(healthBar);
            }
        }

        private void DeleteHealthBar(CharacterProvider character)
        {
            if (_healthBars.TryGetValue(character, out HealthBarProvider healthBar))
            {
                healthBar.Dispose();
                
                _pool.Return(healthBar.View);
            }

            _healthBars.Remove(character);
        }

        private void CreateHealthBar(CharacterProvider character, uint team)
        {
            HealthBarConfiguration config = _configuration.HealthBars.FirstOrDefault(c => c.Team == team);

             if (config != null && character.Health.IsAlive)
             {
                 HealthBarView view = _pool.GetOrCreate(config.HealthBarPrefab);
                 HealthBarProvider healthBarProvider = new HealthBarProvider(character.Health,character.Stats,view);

                 view.Root.SetParent(_sceneData.BattleUi.HealthBarArea);
                 view.Root.localScale = Vector3.one;
                 view.Root.localRotation = Quaternion.identity;
                 
                 _healthBars.Add(character, healthBarProvider);
             }
        }
        
        public void Dispose()
        {
            foreach (CharacterProvider character in _healthBars.Keys.ToList())
            {
                DeleteHealthBar(character);
            }

            _healthBars.Clear();

            _characterFactory.Created -= CreateHealthBar;
            _characterLifecycle.Diad -= DeleteHealthBar;
        }

        private void UpdatePosition(KeyValuePair<CharacterProvider, HealthBarProvider> healthBar)
        {
            Vector2 canvasSizeDelta = _sceneData.BattleUi.HealthBarArea.rect.size;
            Vector3 worldPosition = healthBar.Key.View.Root.position + HealthBarOffset;
            Vector3 viewportPoint = _sceneData.Camera.WorldToViewportPoint(worldPosition);
            Vector2 proportionalPosition = new Vector2(
                viewportPoint.x * canvasSizeDelta.x - canvasSizeDelta.x * 0.5f,
                viewportPoint.y * canvasSizeDelta.y - canvasSizeDelta.y * 0.5f);
            
            healthBar.Value.View.Root.localPosition = proportionalPosition;
        }
    }
}