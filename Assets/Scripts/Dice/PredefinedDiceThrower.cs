using System;
using UnityEngine;

namespace Dice
{
    public class PredefinedDiceThrower : MonoBehaviour
    {
        [SerializeField] private DiceThrowSimulator diceThrowSimulator;
        [SerializeField] private int[] numbers;

        /// <summary>
        /// Вызывает <see cref="DiceThrowSimulator.SimulateDiceThrow"/> с заданными в инспекторе числами
        /// </summary>
        public void ThrowDice() => diceThrowSimulator.SimulateDiceThrow(numbers);

        private void OnValidate()
        {
            for (var i = 0; i < numbers.Length; i++)
                numbers[i] = Math.Clamp(numbers[i], 1, 6);
        }
    }
}