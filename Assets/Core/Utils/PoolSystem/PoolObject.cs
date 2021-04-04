using UnityEngine;

namespace Core.Utils.Pool
{
    public class PoolObject : MonoBehaviour
    {
        public int ID => id;
        [SerializeField] int id;
    }
}
