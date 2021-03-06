using UnityEngine;
using UnityEditor;
using System;

namespace Core.Editor
{
    using Utils;

    [CustomEditor(typeof(Transform), true)]
    [CanEditMultipleObjects]
    public class CustomTransformInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ShowInspectorField("Position",  transform.localPosition,                UpdateLocalPosition,    transform.ResetLocalPosition);
            ShowInspectorField("Rotation",  transform.localRotation.eulerAngles,    UpdateLocalRotation,    transform.ResetLocalRotation);
            ShowInspectorField("Scale",     transform.localScale,                   UpdateLocalScale,       transform.ResetLocalScale);
        }

        private void ShowInspectorField(string fieldName, Vector3 fieldValue, Action<Vector3> updateFunction, Action resetFunction)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            var newFieldValue = EditorGUILayout.Vector3Field(fieldName, fieldValue);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed " + fieldName);
                updateFunction(newFieldValue);
            }

            if (GUILayout.Button("Reset", GUILayout.MaxWidth(70)))
            {
                Undo.RecordObject(target, "Reset " + fieldName);
                resetFunction();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void UpdateLocalPosition(Vector3 localPosition)
        {
            transform.localPosition = localPosition;
        }

        private void UpdateLocalRotation(Vector3 localRotation)
        {
            transform.localRotation = Quaternion.Euler(localRotation);
        }

        private void UpdateLocalScale(Vector3 localScale)
        {
            transform.localScale = localScale;
        }

        private void OnEnable()
        {
            transform = target as Transform;
        }

        private Transform transform;
    }
}