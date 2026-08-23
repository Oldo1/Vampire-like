using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Assets.Scripts.ObjectPool
{
    public class GameObjectPool<T> : System.IDisposable where T : MonoBehaviour
    {
        private readonly ObjectPool<T> _objectPool;
        private readonly HashSet<T> _activeObjects;

        public IEnumerable<T> ActiveObjects => _activeObjects;

        public GameObjectPool(PlaceholderFactory<T> factory)
        {
            _activeObjects = new();
            _objectPool = new ObjectPool<T>(
                createFunc: () => factory.Create(),
                actionOnGet: obj => obj.gameObject.SetActive(true),
                actionOnRelease: obj => obj.gameObject.SetActive(false),
                actionOnDestroy: obj =>
                {
                    if (obj != null)
                        Object.Destroy(obj.gameObject);
                },
                collectionCheck: true,
                maxSize: 1000
            );
        }

        public T Get(Vector3 position)
        {
            var value = _objectPool.Get();
            value.transform.position = position;
            _activeObjects.Add(value);
            return value;
        }

        public void Release(T value)
        {
            _objectPool.Release(value);
            _activeObjects.Remove(value);
        }

        public void Dispose()
        {
            _activeObjects.Clear();
            _objectPool.Dispose();
        }
    }
}
