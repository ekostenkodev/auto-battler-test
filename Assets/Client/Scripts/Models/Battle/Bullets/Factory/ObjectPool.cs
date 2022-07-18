using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scorewarrior.Test.Models
{
    public class ObjectPool<T> where T : Component
    {
        private readonly Dictionary<T, Stack<T>> _pool = new();
        private readonly Dictionary<int, Stack<T>> _rentedHash = new();

        public T GetOrCreate(T prefab)
        {
            if (false == _pool.TryGetValue(prefab, out Stack<T> pool))
            {
                _pool[prefab] = pool = new Stack<T>();
            }

            if (false == pool.TryPop(out T rentable))
            {
                rentable = Object.Instantiate(prefab);
            }

            int hash = rentable.gameObject.GetHashCode();

            _rentedHash[hash] = pool;
            rentable.gameObject.SetActive(true);


            return rentable;
        }

        public void Return(T returnable)
        {
            if (returnable == null)
            {
                return;
            }
            
            int hash = returnable.gameObject.GetHashCode();
            
            if (_rentedHash.TryGetValue(hash, out Stack<T> pool))
            {
                pool.Push(returnable);
                _rentedHash.Remove(hash);
                
                returnable.gameObject.SetActive(false);
            }
        }
    }
}