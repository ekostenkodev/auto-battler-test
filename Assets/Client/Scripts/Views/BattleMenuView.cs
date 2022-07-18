using UnityEngine;
using UnityEngine.UI;

namespace Scorewarrior.Test.Views
{
    public class BattleMenuView : MonoBehaviour
    {
        [field: SerializeField]
        public Button ContinueButton { get; private set; }
		
        [field: SerializeField]
        public Button RestartButton { get; private set; }
    }
}