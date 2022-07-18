using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test
{
    public class SceneData : MonoBehaviour
    {
        [field:SerializeField]
        public Camera Camera { get; set; }
        
        [field:SerializeField]
        public SpawnPointView[] Spawns { get; private set; }
        
        [field:SerializeField]
        public BattleUiView BattleUi { get; private set; }
    }
}