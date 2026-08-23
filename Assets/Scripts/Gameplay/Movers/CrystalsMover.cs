using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class CrystalsMover : ITickable, IFixedTickable, IDisposable
    {
        public bool Enabled { get; set; }

        private readonly float _detectionRadius;
        private readonly LayerMask _crystalsLayerMask;
        private readonly Transform _playerTransform;
        private readonly HashSet<Crystal> _movingCrystals;
        private readonly Collider[] _crystalsCollidersBuffer;

        public CrystalsMover(Transform transform, float detectionRadius, LayerMask layerMask, int bufferLength)
        {
            _playerTransform = transform;
            _movingCrystals = new HashSet<Crystal>();
            Enabled = true;
            _detectionRadius = detectionRadius;
            _crystalsLayerMask = layerMask;
            _crystalsCollidersBuffer = new Collider[bufferLength];
        }

        private void OnCollect(Crystal crystal)
        {
            _movingCrystals.Remove(crystal);
            crystal.OnCollect -= OnCollect;
        }

        private void AddMovingCrystals()
        {
            var playerPosition = _playerTransform.position;
            var overlapResult = Physics.OverlapSphereNonAlloc(playerPosition, _detectionRadius, _crystalsCollidersBuffer, (int)_crystalsLayerMask);
            if (overlapResult != 0)
            {
                for (var i = 0; i < overlapResult; i++)
                {
                    var crystal = _crystalsCollidersBuffer[i].GetComponent<Crystal>();
                    if (!_movingCrystals.Contains(crystal))
                    {
                        _movingCrystals.Add(crystal);
                        crystal.OnCollect += OnCollect;
                    }
                }
            }
        }

        private void MoveCrystals()
        {
            if (_movingCrystals == null)
                throw new NullReferenceException("crystals is null");

            var playerPosition = _playerTransform.position;
            foreach (var crystal in _movingCrystals)
            {
                var crystalTransform = crystal.transform;
                var crystalPosition = crystalTransform.position;
                var direction = (playerPosition - crystalPosition).normalized;
                crystal.Move(direction);
            }
        }

        public void FixedTick()
        {
            if (Enabled)
                AddMovingCrystals();
        }

        public void Tick()
        {
            if (_movingCrystals != null && Enabled)
                MoveCrystals();
        }

        private void UnsubscribeOnCollect()
        {
            foreach (var crystal in _movingCrystals)
                crystal.OnCollect -= OnCollect;
        }

        public void Dispose()
        {
            UnsubscribeOnCollect();
            _movingCrystals.Clear();
        }
    }
}
