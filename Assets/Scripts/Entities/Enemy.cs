using Assets.Scripts.Gameplay;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour, IDamageable, IKillable
    {
        public const string CrystalsParentId = "CrystalsParent";

        public Transform Model { get; set; }
        public Action<float> OnTakeDamage;
        private CrystalObjectPool _crystalPool;
        private EnemyObjectPool _enemyObjectPool;
        private Health _health;
        private CharacterController _characterController;
        private Renderer[] _bodyPartsRenderers;
        private MaterialPropertyBlock _block;
        private HitIndicator _hitIndicator;
        private Transform _crystalsParent;
        private float _speed;

        [Inject]
        public void Construct(CrystalObjectPool crystalPool, EnemyObjectPool enemyObjectPool, Health health, CharacterController characterController,
            float speed, Transform model, Renderer[] bodyPartsRenderers, HitIndicator hitIndicator, MaterialPropertyBlock block,
            [Inject(Id = CrystalsParentId)] Transform crystalsParent)
        {
            _crystalPool = crystalPool;
            _enemyObjectPool = enemyObjectPool;
            _health = health;
            _characterController = characterController;
            _speed = speed;
            Model = model;
            _bodyPartsRenderers = bodyPartsRenderers;
            _hitIndicator = hitIndicator;
            _block = block;
            _crystalsParent = crystalsParent;
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
            if (!isActiveAndEnabled)
                return;

            Flash();
            _health.TakeDamage(damage);
            OnTakeDamage?.Invoke(_health.Progress);
        }

        private void Flash()
        {
            for(var i = 0; i < _bodyPartsRenderers.Length; i++)
            {
                var renderer = _bodyPartsRenderers[i];
                _hitIndicator.Flash(renderer, _block, Color.red, 0.15f, this);
            }
        }

        public void Die()
        {
            if (gameObject.activeSelf)
            {
                _enemyObjectPool.Release(this);
                _crystalPool.Get(transform.position, _crystalsParent);
            }
        }

        private void OnDisable()
        {
            for (var i = 0; i < _bodyPartsRenderers.Length; i++)
                _bodyPartsRenderers[i].SetPropertyBlock(null);
        }

        public class Factory : PlaceholderFactory<Enemy>
        {
        }
    }
}
