using UnityEngine;

namespace Scorewarrior.Test
{
    [CreateAssetMenu(menuName = "Core/HealthBar", fileName = "HealthBar")]
    public class HealthBarConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public int Team { get; private set; }
        
        [field: SerializeField]
        public HealthBarView HealthBarPrefab { get; private set; }
    }
}