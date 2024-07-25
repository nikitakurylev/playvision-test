using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Models;
using UnityEngine;
using Utility;

namespace Dice
{
    /// <summary>
    /// Управляет логикой кости и хранит историю её перемещения
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Die : MonoBehaviour
    {
        [SerializeField] private Transform modelTransform;

        private readonly Queue<PositionRotation> _positionRotationHistory = new();
        private TrailRenderer _trailRenderer;
        private Rigidbody _rigidbody;
        private PositionRotation _startPositionRotation;
        private Quaternion _modelStartLocalRotation;
        private Transform _transform;

        /// <summary>
        /// Находится ли кость в состоянии покоя
        /// </summary>
        public bool IsSleeping => _rigidbody.IsSleeping();

        /// <summary>
        /// Сохраняет текущую позицию и поворот в историю
        /// </summary>
        public void StoreCurrentPositionToHistory()
        {
            _positionRotationHistory.Enqueue(_transform.GetPositionRotation());
        }

        /// <summary>
        /// Применяет к кости самые старые позиции и поворот из истории 
        /// </summary>
        public void ApplyOldestPositionFromHistory()
        {
            if (!_positionRotationHistory.Any())
                return;

            _transform.SetPositionAndRotation(_positionRotationHistory.Dequeue());
        }

        /// <summary>
        /// Очищает историю позиций и поворотов
        /// </summary>
        public void ClearPositionHistory()
        {
            _positionRotationHistory.Clear();
        }

        /// <summary>
        /// Возвращает кость в изначальное положение
        /// </summary>
        public void ResetPosition()
        {
            _transform.SetPositionAndRotation(_startPositionRotation);
        }

        /// <summary>
        /// Возвращает грани кости в изначальное положение
        /// </summary>
        public void ResetFaces()
        {
            modelTransform.localRotation = _modelStartLocalRotation;
            if (_trailRenderer != null)
                _trailRenderer.Clear();
        }

        /// <summary>
        /// Задаёт скорость и угловую скорость кости
        /// </summary>
        /// <param name="velocity">Скорость</param>
        /// <param name="angularVelocity">Угловая скорость</param>
        public void SetVelocity(Vector3 velocity, Vector3 angularVelocity)
        {
            _rigidbody.velocity = velocity;
            _rigidbody.angularVelocity = angularVelocity;
        }

        /// <summary>
        /// Поворачивает модель кости так, чтобы грань с заданным числом оказалась сверху
        /// </summary>
        /// <param name="number">Число, которое должно оказаться на верхней грани</param>
        public void SetTopFaceNumber(int number)
        {
            RotateNumberedFaceUpwards(number);
            FitModelCorners();
        }

        private void RotateNumberedFaceUpwards(int faceNumber)
        {
            switch (faceNumber)
            {
                case 1:
                    modelTransform.right = -Vector3.up;
                    break;
                case 2:
                    modelTransform.forward = -Vector3.up;
                    break;
                case 3:
                    modelTransform.up = -Vector3.up;
                    break;
                case 4:
                    modelTransform.up = Vector3.up;
                    break;
                case 5:
                    modelTransform.forward = Vector3.up;
                    break;
                case 6:
                    modelTransform.right = Vector3.up;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(faceNumber),
                        "Number must be between 1 and 6 (inclusive)");
            }
        }

        private void FitModelCorners()
        {
            modelTransform.localRotation =
                CustomMath.RoundEulerAnglesToMultipleOf90(modelTransform.localRotation.eulerAngles);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _trailRenderer = GetComponent<TrailRenderer>();
            _transform = transform;
            _startPositionRotation = _transform.GetPositionRotation();
            _modelStartLocalRotation = modelTransform.localRotation;
        }
    }
}