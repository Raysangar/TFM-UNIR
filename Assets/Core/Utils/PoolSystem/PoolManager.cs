using UnityEngine;
using System.Collections.Generic;

namespace Core.Utils.Pool
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

        private readonly Dictionary<int, Stack<PoolObject>> pools = new Dictionary<int, Stack<PoolObject>>();

        public T GetInstanceFor<T>(T prefab) where T : PoolObject
        {
            Stack<PoolObject> pool;
            if (!pools.TryGetValue(prefab.ID, out pool))
            {
                pool = new Stack<PoolObject>();
                pools.Add(prefab.ID, pool);
            }

            if (pool.Count > 0)
                return pool.Pop() as T;
            else
                return Instantiate(prefab);
        }

        public void Release(PoolObject instancedObject)
        {
            pools[instancedObject.ID].Push(instancedObject);
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}
