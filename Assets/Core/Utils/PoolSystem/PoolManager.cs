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

        private readonly Dictionary<uint, Stack<PoolObject>> pools = new Dictionary<uint, Stack<PoolObject>>();

        public void Release(PoolObject instancedObject)
        {
            instancedObject.gameObject.SetActive(false);
            var pool = GetPoolFor(instancedObject);
            pool.Push(instancedObject);
        }

        public T GetInstanceFor<T>(T prefab) where T : PoolObject
        {
            var pool = GetPoolFor(prefab);
            return GetOrInstantiate(prefab, pool);
        }

        private Stack<PoolObject> GetPoolFor(PoolObject prefab)
        {
            Stack<PoolObject> pool;
            if (!pools.TryGetValue(prefab.ID, out pool))
            {
                pool = new Stack<PoolObject>();
                pools.Add(prefab.ID, pool);
            }
            return pool;
        }

        private T GetOrInstantiate<T>(T prefab, Stack<PoolObject> pool) where T : PoolObject
        {
            T instancedObject;
            if (pool.Count > 0)
            {
                instancedObject = pool.Pop() as T;
                instancedObject.gameObject.SetActive(true);
            }
            else
            {
                instancedObject = Instantiate(prefab);
                DontDestroyOnLoad(instancedObject);
            }
            return instancedObject;
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}
