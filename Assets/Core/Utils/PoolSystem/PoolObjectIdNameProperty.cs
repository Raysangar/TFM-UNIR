using UnityEngine;

namespace Core.Utils.Pool
{

    public class PoolObjectIdNameProperty : PropertyAttribute
    {
        public PoolObjectIdNames Names {get; private set;}

        public PoolObjectIdNameProperty()
        {
#if UNITY_EDITOR
            var assets = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(PoolObjectIdNames).FullName);
            if (assets.Length > 0)
                Names = UnityEditor.AssetDatabase.LoadAssetAtPath<PoolObjectIdNames>(UnityEditor.AssetDatabase.GUIDToAssetPath(assets[0]));
#endif
        }
    }
}
