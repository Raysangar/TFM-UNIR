using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Utils
{
    public static class TransformExtensionMethods
    {
        public static void Reset(this Transform transform)
        {
            transform.ResetLocalPosition();
            transform.ResetLocalRotation();
            transform.ResetLocalScale();
        }

        #region Position

        public static void ResetLocalPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        public static void SetLocalPositionX(this Transform transform, float position)
        {
            var localPosition = transform.localPosition;
            localPosition.x = position;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionY(this Transform transform, float position)
        {
            var localPosition = transform.localPosition;
            localPosition.y = position;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionZ(this Transform transform, float position)
        {
            var localPosition = transform.localPosition;
            localPosition.z = position;
            transform.localPosition = localPosition;
        }

        #endregion

        #region Rotation

        public static void ResetLocalRotation(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
        }

        public static void SetLocalRotationX(this Transform transform, float degrees)
        {
            var localRotation = transform.localRotation.eulerAngles;
            localRotation.x = degrees;
            transform.localRotation = Quaternion.Euler(localRotation);
        }

        public static void SetLocalRotationY(this Transform transform, float degrees)
        {
            var localRotation = transform.localRotation.eulerAngles;
            localRotation.y = degrees;
            transform.localRotation = Quaternion.Euler(localRotation);
        }

        public static void SetLocalRotationZ(this Transform transform, float degrees)
        {
            var localRotation = transform.localRotation.eulerAngles;
            localRotation.z = degrees;
            transform.localRotation = Quaternion.Euler(localRotation);
        }

        #endregion

        #region Scale

        public static void ResetLocalScale(this Transform transform)
        {
            transform.SetLocalScale(1);
        }

        public static void SetLocalScale(this Transform transform, float scale)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }

        public static void SetLocalScaleX(this Transform transform, float scale)
        {
            var localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;
        }

        public static void SetLocalScaleY(this Transform transform, float scale)
        {
            var localScale = transform.localScale;
            localScale.y = scale;
            transform.localScale = localScale;
        }

        public static void SetLocalScaleZ(this Transform transform, float scale)
        {
            var localScale = transform.localScale;
            localScale.z = scale;
            transform.localScale = localScale;
        }

        #endregion
    }
}
