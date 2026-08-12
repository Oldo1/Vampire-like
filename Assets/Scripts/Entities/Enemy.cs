using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour, IDamageable, IKillable
    {
        public Action<float> OnTakeDamage;
        public Transform Model => _model;

        private CrystalObjectPool _crystalPool;
        private EnemyObjectPool _enemyObjectPool;
        private Health _health;
        private CharacterController _characterController;
        private Transform _model;
        private float _speed;

        [Inject]
        public void Construct(CrystalObjectPool crystalPool, EnemyObjectPool enemyObjectPool, Health health, CharacterController characterController,
            float speed, Transform model)
        {
            _crystalPool = crystalPool;
            _enemyObjectPool = enemyObjectPool;
            _health = health;
            _characterController = characterController;
            _speed = speed;
            _model = model;
        }

        private void OnEnable()
        {
            _health.Restore();
        }

        public void Move(Vector3 direction)
        {
            _characterController.Move(_speed * Time.deltaTime * direction);
        }

        public void TakeDamage(float damage)
        {
            _health.TakeDamage(damage);
            OnTakeDamage?.Invoke(_health.Progress);
        }

        public void Die()
        {
            if (gameObject.activeSelf)
            {
                _enemyObjectPool.Release(this);
                _crystalPool.Get(transform.position);
            }
        }

        public class Factory : PlaceholderFactory<Enemy>
        {
        }
    }
}
