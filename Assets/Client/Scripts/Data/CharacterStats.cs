using System;
using UnityEngine;

namespace Scorewarrior.Test.Data
{
    [Serializable]
    public struct CharacterStats : IStatsProvider<CharacterStats>
    {
        [field: SerializeField]
        public float Accuracy { get; private set; }

        [field: SerializeField]
        public float Dexterity { get; private set; }

        [field: SerializeField]
        public float MaxHealth { get; private set; }

        [field: SerializeField]
        public float MaxArmor { get; private set; }

        [field: SerializeField]
        public float AimTime { get; private set; }

        public CharacterStats(float accuracy, float dexterity, float maxHealth, float maxArmor, float aimTime)
        {
            Accuracy = accuracy;
            Dexterity = dexterity;
            MaxHealth = maxHealth;
            MaxArmor = maxArmor;
            AimTime = aimTime;
        }

        public CharacterStats GetStats()
        {
            return this;
        }
    }
}