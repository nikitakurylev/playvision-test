using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dice
{
    /// <summary>
    /// Симулирует бросок костей
    /// </summary>
    public class DiceThrowSimulator : MonoBehaviour
    {
        [SerializeField] private float simulationStep;
        [SerializeField] private DiceHolder diceHolder;
        [SerializeField] private Vector3 direction;
        [SerializeField] private float minVelocity, maxVelocity;

        /// <summary>
        /// Симулирует бросок костей и меняет грани так, чтобы выпали заданные числа
        /// </summary>
        /// <param name="numbers">Числа, которые должны выпасть</param>
        /// <exception cref="ArgumentOutOfRangeException">Выдаётся, если чисел больше, чем костей, предоставленных DiceHolder</exception>
        public void SimulateDiceThrow(IReadOnlyList<int> numbers)
        {
            var dice = diceHolder.GetDice();

            if (numbers.Count > dice.Length)
                throw new ArgumentOutOfRangeException(nameof(numbers), "More numbers provided than there are dice");

            ResetDice(dice);

            //Далее работаем только с необходимым количеством костей
            dice = dice.Take(numbers.Count).ToArray();

            SetDiceVelocity(dice);

            SimulateDiceAndStorePosition(dice);

            for (var i = 0; i < numbers.Count; i++)
            {
                dice[i].SetTopFaceNumber(numbers[i]);
                dice[i].ResetPosition();
            }
        }

        private static void ResetDice(IEnumerable<Die> dice)
        {
            foreach (var die in dice)
            {
                die.ClearPositionHistory();
                die.ResetPosition();
                die.ResetFaces();
            }
        }

        private void SetDiceVelocity(IEnumerable<Die> dice)
        {
            foreach (var die in dice)
                die.SetVelocity(
                    direction * Random.Range(minVelocity, maxVelocity),
                    Random.onUnitSphere * Random.Range(minVelocity, maxVelocity)
                );
        }

        private void SimulateDiceAndStorePosition(IReadOnlyCollection<Die> dice)
        {
            while (dice.Any(d => !d.IsSleeping))
            {
                Physics.Simulate(simulationStep);
                foreach (var die in dice)
                    die.StoreCurrentPositionToHistory();
            }
        }
    }
}