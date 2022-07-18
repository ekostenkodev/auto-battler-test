using System;
using Scorewarrior.Test.Data;
using Scorewarrior.Test.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scorewarrior.Test.Models
{
    public class BulletFactory : IBulletFactory
    {
        private readonly Vector3 CharacterOffset = new(0, 2, 0);
        private readonly ObjectPool<BulletView> _pool = new();

        public event Action<BulletProvider> Created;

        public BulletProvider Create(CharacterProvider from, CharacterProvider to)
        {
            BulletView view = _pool.GetOrCreate(from.Weapon.View.BulletView);
            bool isApplied = IsHitApplied(from, to);
            WeaponStats weaponStats = from.Weapon.Model.Stats.GetStats();
            Vector3 targetPosition = to.View.Root.position + CharacterOffset;
            Transform barrelRoot = from.Weapon.View.BarrelRoot;
            float speed = weaponStats.BulletSpeed;
            float damage = weaponStats.Damage;

            view.Root.position = barrelRoot.position;
            view.Root.rotation = barrelRoot.rotation;
            view.Trail.Clear();

            BulletModel model = new BulletModel(to.Health, isApplied, targetPosition, speed, damage);
            BulletProvider provider = new BulletProvider(model, view);

            Created?.Invoke(provider);

            return provider;
        }

        public void Destroy(BulletProvider bullet)
        {
            bullet.View.Trail.Clear();
            _pool.Return(bullet.View);
        }

        private bool IsHitApplied(CharacterProvider from, CharacterProvider to)
        {
            float random = Random.value;
            bool hit = random <= from.Stats.GetStats().Accuracy &&
                       random <= from.Weapon.Model.Stats.GetStats().Accuracy &&
                       random >= to.Stats.GetStats().Dexterity;

            return hit;
        }
    }
}