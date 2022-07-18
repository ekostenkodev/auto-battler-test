using Scorewarrior.Test.Data;
using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test
{
    [CreateAssetMenu(menuName = "Core/Character", fileName = "Character")]
    public class CharacterConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public CharacterView View { get; private set; }

        [field: SerializeField]
        public CharacterStats Stats { get; private set; }

        [field: SerializeField]
        public WeaponConfiguration Weapon { get; private set; }
    }
}