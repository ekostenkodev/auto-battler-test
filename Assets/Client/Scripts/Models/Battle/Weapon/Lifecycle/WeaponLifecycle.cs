using System;
using System.Collections.Generic;
using Zenject;

namespace Scorewarrior.Test.Models
{
    public class WeaponLifecycle : IWeaponLifecycle, IDisposable
    {
        private readonly List<WeaponProvider> _weapons = new();
        
        private IGameTime _gameTime;
        private IWeaponFactory _weaponFactory;

        [Inject]
        public void Construct(IGameTime gameTime, IWeaponFactory factory)
        {
            _gameTime = gameTime;
            _weaponFactory = factory;
            
            _weaponFactory.Created += OnCreate;
        }
        
        public void Dispose()
        {
            _weaponFactory.Created -= OnCreate;
        }

        private void OnCreate(WeaponProvider weapon)
        {
            _weapons.Add(weapon);
        }

        public void Update()
        {
            foreach (WeaponProvider weapon in _weapons)
            {
                weapon.Model.Update(_gameTime.DeltaTime);
            }
        }
    }
}