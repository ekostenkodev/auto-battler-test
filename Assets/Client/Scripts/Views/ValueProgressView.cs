using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scorewarrior.Test.Views
{
    public class ValueProgressView : MonoBehaviour
    {
        [field: SerializeField]
        public TMP_Text Text { get; private set; }
        
        [field: SerializeField]
        public Slider Slider { get; private set; }
    }
}