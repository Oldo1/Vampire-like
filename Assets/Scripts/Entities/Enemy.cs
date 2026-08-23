using Assets.Scripts.Gameplay;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour, IDamageable, IKillable
    {
        private Color _baseColor;

        public Transform Model { get; set; }
        public Action<float> OnTakeDamage;
        private CrystalObjectPool _crystalPool;
        private EnemyObjectPool _enemyObjectPool;
        private Health _health;
        private CharacterController _characterController;
        private Renderer[] _bodyPartsRenderers;
        private HitIndicator _hitIndicator;
        private float _speed;

        public IEnumerable<Renderer> BodyPartsRenderers => _bodyPartsRenderers;

        [Inject]
        public void Construct(CrystalObjectPool crystalPool, EnemyObjectPool enemyObjectPool, Health health, CharacterController characterController,
            float speed, Transform model, Renderer[] bodyPartsRenderers, HitIndicator hitIndicator)
        {
            _crystalPool = crystalPool;
            _enemyObjectPool = enemyObjectPool;
            _health = health;
            _characterController = characterController;
            _speed = speed;
            Model = model;
            _bodyPartsRenderers = bodyPartsRenderers;
            _hitIndicator = hitIndicator;
            _baseColor = Color.white;
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
            Flash();
            _health.TakeDamage(damage);
            OnTakeDamage?.Invoke(_health.Progress);
        }

        private void Flash()
        {
            for(var i = 0; i < _bodyPartsRenderers.Length; i++)
            {
                var material = _bodyPartsRenderers[i].material;
                _hitIndicator.Flash(material, Color.red, _baseColor, 0.15f, this);
            }
        }

        public void Die()
        {
            if (gameObject.activeSelf)
            {
                _enemyObjectPool.Release(this);
                _crystalPool.Get(transform.position);
            }
        }

        private void OnDisable()
        {
            for (var i = 0; i < _bodyPartsRenderers.Length; i++)
            {
                var render = _bodyPartsRenderers[i];
                render.material.color = _baseColor;
            }
        }

        public class Factory : PlaceholderFactory<Enemy>
        {
        }
    }
}
