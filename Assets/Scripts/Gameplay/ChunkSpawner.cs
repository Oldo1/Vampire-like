using Assets.Scripts.Entities;
using Assets.Scripts.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class ChunkSpawner : IFixedTickable
    {
        private readonly Transform _groundTransform;
        private readonly Player _player;
        private readonly ChunkObjectPool _chunkObjectPool;
        private readonly float _raycastMaxDistance;
        private readonly LayerMask _chunkLayerMask;
        private readonly float _spawnRadius;

        private Transform _currentChunk;

        private readonly Dictionary<Vector3, Transform> _createdChunks;
        private readonly List<Vector3> _chunksPositionsToRemove;

        private readonly Vector3[] _offsets = new Vector3[]
        {
            new Vector3(0, 0, 40),
            new Vector3(0, 0, -40),
            new Vector3(50, 0, 0),
            new Vector3(-50, 0, 0),
            new Vector3(50, 0, 40),
            new Vector3(50, 0, -40),
            new Vector3(-50, 0, 40),
            new Vector3(-50, 0, -40)
        };

        public ChunkSpawner(Chunk currentChunk, Transform groundTransform, float raycastMaxDistance, LayerMask chunkLayerMask, Player player, ChunkObjectPool chunkObjectPool)
        {
            _currentChunk = currentChunk.transform;
            _groundTransform = groundTransform;
            _raycastMaxDistance = raycastMaxDistance;
            _chunkLayerMask = chunkLayerMask;
            _player = player;
            _chunkObjectPool = chunkObjectPool;
            _createdChunks = new();
            _spawnRadius = 100;
            _createdChunks.Add(_currentChunk.transform.localPosition, _currentChunk.transform);
            _chunksPositionsToRemove = new List<Vector3>(capacity: 8);
        }

        public void FixedTick()
        {
            var playerPosition = _player.transform.position;
            var isHit = Physics.Raycast(playerPosition, Vector3.down, out var raycastHit, _raycastMaxDistance, (int)_chunkLayerMask);
            if (!isHit) return;

            var hitChunk = raycastHit.transform.GetComponentInParent<Chunk>();
            if (hitChunk == null) return;
            if (hitChunk.transform == _currentChunk && _createdChunks.Count > 1) return;

            _currentChunk = hitChunk.transform;

            RemoveDistantChunks();
            SpawnMissingChunks();
        }

        private void SpawnMissingChunks()
        {
            foreach (var offset in _offsets)
            {
                var spawnPosition = _currentChunk.transform.localPosition + offset;
                if (!_createdChunks.ContainsKey(spawnPosition))
                {
                    var chunk = _chunkObjectPool.Get(Vector3.zero);
                    chunk.transform.SetParent(_groundTransform, false);
                    chunk.transform.SetLocalPositionAndRotation(spawnPosition, Quaternion.identity);
                    _createdChunks.Add(spawnPosition, chunk.transform);
                }
            }
        }

        private void RemoveDistantChunks()
        {
            foreach (var position in _createdChunks.Keys)
            {
                var currentChunkPosition = _currentChunk.transform.localPosition;
                if (Vector3.Distance(currentChunkPosition, position) <= _spawnRadius) continue;

                var chunk = _createdChunks[position];
                if (chunk == _currentChunk) continue;

                _chunksPositionsToRemove.Add(position);
            }

            foreach (var position in _chunksPositionsToRemove)
            {
                if (_createdChunks.TryGetValue(position, out var chunk))
                {
                    _chunkObjectPool.Release(chunk.GetComponent<Chunk>());
                    _createdChunks.Remove(position);
                }
            }
            _chunksPositionsToRemove.Clear();
        }
    }
}
