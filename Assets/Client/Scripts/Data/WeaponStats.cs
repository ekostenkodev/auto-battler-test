using System;
using UnityEngine;

namespace Scorewarrior.Test.Data
{
    [Serializable]
    public struct WeaponStats : IStatsProvider<WeaponStats>
    {
        [field: SerializeField]
        public float Damage { get; private set; }

        [field: SerializeField]
        public float Accuracy { get; private set; }

        [field: SerializeField]
        public float FireRate { get; private set; }

        [field: SerializeField]
        public uint ClipSize { get; private set; }

        [field: SerializeField]
        public float ReloadTime { get; private set; }

        [field: SerializeField]
        public float BulletSpeed { get; private set; }

        public WeaponStats(
            float damage, 
            float accuracy, 
            float fireRate, 
            uint clipSize, 
            float reloadTime, 
            float bulletSpeed)
        {
            Damage = damage;
            Accuracy = accuracy;
            FireRate = fireRate;
            ClipSize = clipSize;
            ReloadTime = reloadTime;
            BulletSpeed = bulletSpeed;
        }

        public WeaponStats GetStats()
        {
            return this;
        }
    }
}