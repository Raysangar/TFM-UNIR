using UnityEngine;
using System.Collections.Generic;

namespace Core.Utils
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var gameObject = new GameObject("PoolManager");
                    gameObject.transform.Reset();
                    DontDestroyOnLoad(gameObject);
                    instance = gameObject.AddComponent<PoolManager>();
                }
                return instance;
            }
        }

        private static PoolManager instance;

        private readonly Dictionary<System.Type, Stack<MonoBehaviour>> pools = new Dictionary<System.Type, Stack<MonoBehaviour>>();

        public T GetInstanceFor<T>(T prefab) where T : MonoBehaviour
        {
            var prefabType = typeof(T);
            Stack<MonoBehaviour> pool;

            if (!pools.TryGetValue(prefabType, out pool))
            {
                pool = new Stack<MonoBehaviour>();
                pools.Add(prefabType, pool);
            }

            if (pool.Count > 0)
                return pool.Pop() as T;
            else
                return Instantiate(prefab);
        }

        public void Release<T>(T instancedObject) where T : MonoBehaviour
        {
            pools[typeof(T)].Push(instancedObject);
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}
