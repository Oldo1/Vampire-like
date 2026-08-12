using UnityEngine;
using System;
using Zenject;

namespace Assets.Scripts
{
    public class Crystal : MonoBehaviour
    {
        public float XpToAdd { get; private set; }
        public event Action<Crystal> OnCollect;

        private void Start()
        {
            XpToAdd = 1;
        }

        private void OnDisable()
        {
            OnCollect?.Invoke(this);
        }

        public void Move(Vector3 direction)
        {
            transform.Translate(direction * (5 * Time.deltaTime));
        }

        public class Factory : PlaceholderFactory<Crystal>
        {
        }
    }
}
