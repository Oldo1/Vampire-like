using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Health
    {
        public float Value { get; private set; }
        public float Progress => Value / _maxHealth;

        private readonly IKillable _killable;
        private readonly float _maxHealth;

        public Health(float maxHealth, IKillable killable)
        {
            if (maxHealth <= 0)
                throw new ArgumentOutOfRangeException("max health less or equals 0");
            Value = maxHealth;
            _killable = killable;
            _maxHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException("damage less or equals 0");

            Value = Mathf.MoveTowards(Value, 0, damage);

            if (Value == 0)
                _killable.Die();
        }

        public void Restore()
        {
            Value = _maxHealth;
        }
    }
}
