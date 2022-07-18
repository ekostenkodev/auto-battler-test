using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test
{
    public class HealthBarView : MonoBehaviour
    {
        [field: SerializeField]
        public RectTransform Root { get; private set; }
        
        [field: SerializeField]
        public ValueProgressView Health { get; private set; }

        [field: SerializeField]
        public ValueProgressView Armor { get; private set; }
    }
}