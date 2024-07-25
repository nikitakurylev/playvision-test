using UnityEngine;

namespace Models
{
   /// <summary>
   /// Позиция и поворот
   /// </summary>
   /// <param name="Position">Позиция</param>
   /// <param name="Rotation">Поворот</param>
   public record PositionRotation(Vector3 Position, Quaternion Rotation);
}