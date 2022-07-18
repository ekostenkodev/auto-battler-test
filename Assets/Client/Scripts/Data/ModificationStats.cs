using System;
using Scorewarrior.Test.Data;
using UnityEngine;

namespace Scorewarrior.Test
{
    [Serializable]
    public class ModificationStats<T> where T : IStatsProvider<T>
    {
        public enum ModifierType
        {
            None = 0,
            Add = 1,
            PercentAdd = 2,
            Multiply = 3,
        }
        
        [field: SerializeField]
        public string Id { get; private set; }
        
        [field: SerializeField]
        public ModifierType Type { get; private set; }
        
        [field: SerializeField]
        public T Modificator { get; private set; }
    }
}