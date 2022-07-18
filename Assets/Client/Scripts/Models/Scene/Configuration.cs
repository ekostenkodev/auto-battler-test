using Scorewarrior.Test.Data;
using UnityEngine;

namespace Scorewarrior.Test
{
    [CreateAssetMenu(menuName = "Core/Configuration", fileName = "Configuration")]
    public class Configuration : ScriptableObject
    {
        [Header("Characters")]
        [SerializeField]
        private CharacterConfiguration[] _characters;
        
        [SerializeField]
        private HealthBarConfiguration[] _healthBars;

        [Header("Character modifications")]
        [SerializeField]
        private int _characterModificationMaxCount;
        
        [SerializeField]
        private ModificationStats<CharacterStats>[] _characterModifications;

        [Space(10)]
        [Header("Weapon modifications")]
        [SerializeField]
        public int _weaponModificationMaxCount;
        
        [SerializeField]
        public ModificationStats<WeaponStats>[] _weaponModifications;


        public CharacterConfiguration[] Characters => _characters;
        public HealthBarConfiguration[] HealthBars => _healthBars;
        public int CharacterModificationMaxCount => _characterModificationMaxCount;
        public ModificationStats<CharacterStats>[] CharacterModifications => _characterModifications;
        public int WeaponModificationMaxCount => _weaponModificationMaxCount;
        public ModificationStats<WeaponStats>[] WeaponModifications => _weaponModifications;
    }
}