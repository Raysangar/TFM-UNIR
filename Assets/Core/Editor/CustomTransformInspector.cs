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
            serializedObject.Update();

            ShowInspectorField(localPosition,  ResetPositions);
            ShowInspectorField(localRotation,  ResetRotations);
            ShowInspectorField(localScale,     ResetScales);

            serializedObject.ApplyModifiedProperties();
        }

        private void ShowInspectorField(SerializedProperty fieldValue, Action resetFunction)
        {
            EditorGUILayout.BeginHorizontal();


            if (fieldValue.displayName == "Local Rotation")
                RotationPropertyField(fieldValue);
            else
                Vector3PropertyField(fieldValue);
            

            if (GUILayout.Button("Reset", GUILayout.MaxWidth(70)))
            {
                Undo.RecordObject(target, "Reset " + fieldValue.displayName);
                resetFunction();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void RotationPropertyField(SerializedProperty rotationProperty)
        {
            Transform transform = targets[0] as Transform;
            Quaternion localRotation = transform.localRotation;
            foreach (var t in targets)
            {
                if (!SameRotation(localRotation, (t as Transform).localRotation))
                {
                    EditorGUI.showMixedValue = true;
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();

            Vector3 eulerAngles = EditorGUILayout.Vector3Field("Rotation", localRotation.eulerAngles);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(targets, "Rotation Changed");
                foreach (var obj in targets)
                    (obj as Transform).localEulerAngles = eulerAngles;
                rotationProperty.serializedObject.SetIsDifferentCacheDirty();
            }

            EditorGUI.showMixedValue = false;
        }

        private void Vector3PropertyField(SerializedProperty fieldValue)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(fieldValue);
            if (EditorGUI.EndChangeCheck())
                Undo.RecordObject(target, "Changed " + fieldValue.displayName);
        }

        private bool SameRotation(Quaternion rot1, Quaternion rot2)
        {
            if (rot1.x != rot2.x) return false;
            if (rot1.y != rot2.y) return false;
            if (rot1.z != rot2.z) return false;
            if (rot1.w != rot2.w) return false;
            return true;
        }

        private void ResetPositions()
        {
            foreach (var target in targets)
                (target as Transform).ResetLocalPosition();
        }

        private void ResetRotations()
        {
            foreach (var target in targets)
                (target as Transform).ResetLocalRotation();
        }

        private void ResetScales()
        {
            foreach (var target in targets)
                (target as Transform).ResetLocalScale();
        }

        private void OnEnable()
        {
            localPosition = serializedObject.FindProperty("m_LocalPosition");
            localRotation = serializedObject.FindProperty("m_LocalRotation");
            localScale = serializedObject.FindProperty("m_LocalScale");
        }

        private SerializedProperty localPosition;
        private SerializedProperty localRotation;
        private SerializedProperty localScale;
    }
}