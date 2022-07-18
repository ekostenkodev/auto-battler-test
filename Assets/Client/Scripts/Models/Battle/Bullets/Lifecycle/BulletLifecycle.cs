using System;
using System.Collections.Generic;
using Scorewarrior.Test.Models;
using UnityEngine;
using Zenject;

namespace Scorewarrior.Test.Views
{
    public class BulletLifecycle : IBulletLifecycle, IDisposable
    {
        private readonly List<BulletProvider> _bullets = new();
        private readonly List<BulletProvider> _toDelete = new();
        
        private IBulletFactory _bulletFactory;
        private IGameTime _gameTime;

        [Inject]
        private void Construct(IGameTime gameTime, IBulletFactory bulletFactory)
        {
            _gameTime = gameTime;
            _bulletFactory = bulletFactory;
            
            _bulletFactory.Created += OnCreate;
        }
        
        public void Dispose()
        {
            _bulletFactory.Created -= OnCreate;
        }

        private void OnCreate(BulletProvider bullet)
        {
            _bullets.Add(bullet);
        }

        public void Update()
        {
            foreach (BulletProvider bullet in _bullets)
            {
                ApplyBulletMovement(bullet);
            }

            ClearExpiredBullets();
        }

        private void ApplyBulletMovement(BulletProvider bullet)
        {
            Transform bulletRoot = bullet.View.Root;
            Vector3 targetPosition = bullet.Model.Target;
            float speed = bullet.Model.Speed * _gameTime.DeltaTime;
            Vector3 nextPosition = Vector3.MoveTowards(bulletRoot.position, targetPosition, speed);

            bulletRoot.position = nextPosition;

            if (bullet.View.Root.position == bullet.Model.Target)
            {
                bullet.Model.To.ApplyDamage(bullet.Model.Damage);
                
                Destroy(bullet);
            }
        }
        
        private void ClearExpiredBullets()
        {
            foreach (BulletProvider bullet in _toDelete)
            {
                _bullets.Remove(bullet);
            }

            _toDelete.Clear();
        }

        private void Destroy(BulletProvider bullet)
        {
            _toDelete.Add(bullet);
            _bulletFactory.Destroy(bullet);
        }
    }
}