using Assets.Scripts.PlayerScripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class CrystalsMover : ITickable, IFixedTickable, IDisposable
    {
        public bool Enabled { get; set; }

        private readonly Transform _playerTransform;
        private readonly CrystalsDetection _crystalsDetection;

        private HashSet<Crystal> _movingCrystals;

        public CrystalsMover(Transform transform, CrystalsDetection crystalsDetection)
        {
            _playerTransform = transform;
            _crystalsDetection = crystalsDetection;
            _movingCrystals = new HashSet<Crystal>();
            Enabled = true;
        }

        private void OnCollect(Crystal crystal)
        {
            _movingCrystals.Remove(crystal);
            crystal.OnCollect -= OnCollect;
        }

        private void AddNerbyCrystals()
        {
            if (_crystalsDetection.TryGetCrystalsNearbyCrystals(out var crystals))
            {
                foreach (var crystal in crystals)
                {
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
                AddNerbyCrystals();
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
