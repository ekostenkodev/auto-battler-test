using UnityEngine;

namespace Scorewarrior.Test.Views
{
    public class BattleUiView : MonoBehaviour
    {
        [field:SerializeField]
        public BattleMenuView BattleMenu { get; private set; }
        
        [field:SerializeField]
        public RectTransform HealthBarArea { get; private set; }
    }
}