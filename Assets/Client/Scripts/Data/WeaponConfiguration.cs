using Scorewarrior.Test.Data;
using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test
{
    [CreateAssetMenu(menuName = "Core/Weapon", fileName = "Weapon")]
    public class WeaponConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public WeaponView View { get; private set; }

        [field: SerializeField]
        public WeaponStats Stats { get; private set; }
    }
}