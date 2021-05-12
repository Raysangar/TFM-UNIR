using UnityEngine;

namespace Core.Utils.Pool
{
    [CreateAssetMenu(fileName = "PoolObjectIdNames", menuName = "Core/Pool/Object Id Names")]
    public class PoolObjectIdNames : ScriptableObject
    {
        public string[] All => names;
        [SerializeField] string[] names;
    }
}
