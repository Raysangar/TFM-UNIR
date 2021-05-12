using UnityEngine;
using UnityEditor;

namespace Core.Utils.Pool
{
    [CustomPropertyDrawer(typeof(PoolObjectIdNameProperty))]
    public class PoolObjectIdNamePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                var names = (attribute as PoolObjectIdNameProperty).Names.All;
                property.intValue = EditorGUI.Popup(position, "ID", property.intValue, names);
            }
        }
    }
}
