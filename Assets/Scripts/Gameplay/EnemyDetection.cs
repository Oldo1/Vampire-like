using Assets.Scripts.Configs;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyDetection
    {
        private readonly LayerMask _enemyLayerMask;
        private readonly Collider[] _enemyCollidersBuffer;

        public EnemyDetection(DetectionConfig enemyDetectionConfig)
        {
            if (enemyDetectionConfig == null)
                throw new NullReferenceException("enemyDetectionConfig is null");
            if (enemyDetectionConfig.DetectionRadius < 0)
                throw new ArgumentOutOfRangeException("detectionRadius less than 0");
            if (enemyDetectionConfig.BufferLength < 0)
                throw new ArgumentOutOfRangeException("bufferLength less than 0");

            _enemyLayerMask = enemyDetectionConfig.LayerMask;
            _enemyCollidersBuffer = new Collider[enemyDetectionConfig.BufferLength];
        }

        public bool TryGetEnemiesInRadius(Vector3 position, float radius, out Collider[] enemies, out int count)
        {
            count = Physics.OverlapSphereNonAlloc(position, radius, _enemyCollidersBuffer, (int)_enemyLayerMask);
            enemies = _enemyCollidersBuffer;
            return count != 0;
        }
    }
}
