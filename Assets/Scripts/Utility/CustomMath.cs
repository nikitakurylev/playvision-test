using UnityEngine;

namespace Utility
{
    public static class CustomMath
    {
        /// <summary>
        /// Округляет каждую ось поворта до ближайшего числиа, кратного 90
        /// </summary>
        public static Quaternion RoundEulerAnglesToMultipleOf90(Vector3 eulerAngles) => Quaternion.Euler(
            RoundToMultipleOf90(eulerAngles.x),
            RoundToMultipleOf90(eulerAngles.y),
            RoundToMultipleOf90(eulerAngles.z));

        private static float RoundToMultipleOf90(float f) => Mathf.Round(f / 90) * 90;
    }
}