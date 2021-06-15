using UnityEngine;

namespace Core.Utils.Pool
{
    public class PoolObject : MonoBehaviour
    {
        public uint ID => id;
        public bool ShouldBeReleasedBackToThePool => shouldBeReleasedBackToThePool;

        [SerializeField, PoolObjectIdNameProperty] uint id;
        [SerializeField] bool shouldBeReleasedBackToThePool;
    }
}
