using UnityEngine;

namespace Dice
{
    /// <summary>
    /// Двигает кости по их историям позиции и вращения
    /// </summary>
    public class DiceUpdater : MonoBehaviour
    {
        [SerializeField] private DiceHolder diceHolder;
        [SerializeField] private float playbackFrameTime;

        private Die[] _dice;
        private float _nextUpdateTime;

        private void Awake()
        {
            _dice = diceHolder.GetDice();
        }
    
        private void Update()
        {
            if (_nextUpdateTime >= Time.time) return;

            _nextUpdateTime = Time.time + playbackFrameTime;
            foreach (var die in _dice)
                die.ApplyOldestPositionFromHistory();
        }
    }
}