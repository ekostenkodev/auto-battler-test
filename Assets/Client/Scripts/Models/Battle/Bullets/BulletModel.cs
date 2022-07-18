using System;
using Scorewarrior.Test.Models;
using UnityEngine;

namespace Scorewarrior.Test.Views
{
    public class BulletModel
    {
        public HealthComponent To { get; }

        public Vector3 Target { get; }
        public bool IsApplied { get; }
        public float Speed { get; }
        public float Damage { get; }

        public BulletModel(
            HealthComponent to,
            bool isApplied,
            Vector3 target,
            float speed,
            float damage)
        {
            To = to;
            IsApplied = isApplied;
            Target = target;
            Speed = speed;
            Damage = damage;
        }
    }
}