using Models;
using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static void SetPositionAndRotation(this Transform transform, PositionRotation positionRotation) =>
            transform.SetPositionAndRotation(positionRotation.Position, positionRotation.Rotation);

        public static PositionRotation GetPositionRotation(this Transform transform) =>
            new(transform.position, transform.rotation);
    }
}