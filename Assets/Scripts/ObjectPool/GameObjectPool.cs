using System;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Assets.Scripts.ObjectPool
{
    public class GameObjectPool<T> : IDisposable, IInitializable where T : MonoBehaviour
    {
        private readonly ObjectPool<T> _objectPool;
        private readonly int _preloadCount;
        private readonly Transform _parent;

        public event Action<T> OnGet;
        public event Action<T> OnRelease;

        public GameObjectPool(PlaceholderFactory<T> factory)
        {
            _objectPool = new ObjectPool<T>(
                createFunc: () => factory.Create(),
                actionOnGet: null,
                actionOnRelease: obj => obj.gameObject.SetActive(false),
                actionOnDestroy: obj =>
                {
                    if (obj != null)
                        UnityEngine.Object.Destroy(obj.gameObject);
                },
                collectionCheck: true,
                maxSize: 1000
            );
        }

        public GameObjectPool(PlaceholderFactory<T> factory, int preloadCount, Transform parent) : this(factory)
        {
            _preloadCount = preloadCount;
            _parent = parent;
        }

        public void Initialize()
        {
            if (_preloadCount > 0)
                Preload(_preloadCount, _parent);
        }

        private void Preload(int count, Transform parent)
        {
            if (count < 0)
                throw new System.ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");

            var objects = new T[count];

            for (var i = 0; i < count; i++)
                objects[i] = Get(Vector3.zero, parent);

            for (var i = 0; i < count; i++)
                Release(objects[i]);
        }

        public T Get(Vector3 position)
        {
            var value = _objectPool.Get();
            value.transform.position = position;
            value.gameObject.SetActive(true);
            OnGet?.Invoke(value);
            return value;
        }

        public T Get(Vector3 position, Transform transform)
        {
            var value = Get(position);
            value.transform.SetParent(transform);
            return value;
        }

        public void Release(T value)
        {
            _objectPool.Release(value);
            OnRelease?.Invoke(value);
        }

        public void Dispose()
        {
            _objectPool.Dispose();
        }
    }
}
