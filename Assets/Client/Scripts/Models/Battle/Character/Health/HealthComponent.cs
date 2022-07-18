using System;
using UnityEngine;

namespace Scorewarrior.Test.Models
{
    public class HealthComponent
    {
        public float Health { get; private set; }
        public float Armor { get; private set; }

        public bool IsAlive => Health > 0;
        
        public event Action Changed;
        
        public HealthComponent(float health, float armor)
        {
            Health = health;
            Armor = armor;
        }

        public void ApplyDamage(float value)
        {
            if (Armor > 0)
            {
                float armorChange = Mathf.Min(value, Armor);
                Armor = Mathf.Clamp(Armor - armorChange, 0f, float.MaxValue);
                value -= armorChange;
            }

            if (value > 0)
            {
                Health -= value;
            }

            Changed?.Invoke();
        }
    }
}