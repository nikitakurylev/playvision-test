using UnityEngine;

namespace Dice
{
    /// <summary>
    /// Хранит кости и предоставляет к ним доступ
    /// </summary>
    public class DiceHolder : MonoBehaviour
    {
        [SerializeField] private Die[] dice;

        public Die[] GetDice() => dice;
    }
}