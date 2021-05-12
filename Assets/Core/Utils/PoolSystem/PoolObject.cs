using UnityEngine;

namespace Core.Utils.Pool
{
    public class PoolObject : MonoBehaviour
    {
        public uint ID => id;
        [SerializeField, PoolObjectIdNameProperty] uint id;
    }
}
