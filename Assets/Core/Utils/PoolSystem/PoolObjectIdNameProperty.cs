using UnityEngine;
using UnityEditor;

namespace Core.Utils.Pool
{

    public class PoolObjectIdNameProperty : PropertyAttribute
    {
        public PoolObjectIdNames Names {get; private set;}

        public PoolObjectIdNameProperty()
        {
            var assets = AssetDatabase.FindAssets("t:" + typeof(PoolObjectIdNames).FullName);
            if (assets.Length > 0)
                Names = AssetDatabase.LoadAssetAtPath<PoolObjectIdNames>(AssetDatabase.GUIDToAssetPath(assets[0]));
        }
    }
}
