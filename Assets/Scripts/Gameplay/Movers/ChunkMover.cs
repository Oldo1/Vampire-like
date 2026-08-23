using Assets.Scripts.Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class ChunkMover : IInitializable, IFixedTickable
    {
        private const int RequiredChunksCount = 9;

        private readonly Player _player;
        private readonly float _raycastMaxDistance;
        private readonly LayerMask _chunkLayerMask;

        private readonly HashSet<Transform> _chunksTransform;
        private readonly Dictionary<Vector3, Transform> _chunksByPosition = new(RequiredChunksCount);
        private readonly HashSet<Vector3> _requiredPositions = new();

        private Transform _currentChunk;

        private readonly Vector3[] _offsets =
        {
            Vector3.zero,
            new Vector3(0, 0, 40),
            new Vector3(0, 0, -40),
            new Vector3(50, 0, 0),
            new Vector3(-50, 0, 0),
            new Vector3(50, 0, 40),
            new Vector3(50, 0, -40),
            new Vector3(-50, 0, 40),
            new Vector3(-50, 0, -40)
        };

        public ChunkMover(
            Chunk[] chunks,
            float raycastMaxDistance,
            LayerMask chunkLayerMask,
            Player player)
        {
            _raycastMaxDistance = raycastMaxDistance;
            _chunkLayerMask = chunkLayerMask;
            _player = player;
            _chunksTransform = chunks.Select(x => x.transform).ToHashSet();
        }

        public void Initialize()
        {
            if (_chunksTransform.Count < RequiredChunksCount)
            {
                Debug.LogError(
                    $"ChunkMover requires at least {RequiredChunksCount} explicitly assigned chunks, " +
                    $"but received {_chunksTransform.Count}.");
                return;
            }

            if (!TryGetChunkUnderPlayer(out _currentChunk))
            {
                Debug.LogError("ChunkMover could not find a Chunk under the player during initialization.");
                return;
            }

            MoveChunksAroundCurrent();
        }

        public void FixedTick()
        {
            if (_chunksTransform.Count < RequiredChunksCount)
                return;

            if (!TryGetChunkUnderPlayer(out var hitChunk))
                return;

            if (hitChunk == _currentChunk)
                return;

            _currentChunk = hitChunk;
            MoveChunksAroundCurrent();
        }

        private bool TryGetChunkUnderPlayer(out Transform chunkTransform)
        {
            chunkTransform = null;

            var playerPosition = _player.transform.position;
            var isHit = Physics.Raycast(
                playerPosition,
                Vector3.down,
                out var raycastHit,
                _raycastMaxDistance,
                _chunkLayerMask);

            if (!isHit)
                return false;

            var chunk = raycastHit.collider.GetComponentInParent<Chunk>();
            if (chunk == null || !_chunksTransform.Contains(chunk.transform))
                return false;

            chunkTransform = chunk.transform;
            return true;
        }

        private void MoveChunksAroundCurrent()
        {
            var currentPosition = _currentChunk.localPosition;

            _requiredPositions.Clear();
            foreach (var offset in _offsets)
                _requiredPositions.Add(currentPosition + offset);

            RebuildChunksByPosition();

            foreach (var offset in _offsets)
            {
                var requiredPosition = currentPosition + offset;
                if (_chunksByPosition.ContainsKey(requiredPosition))
                    continue;

                var chunkToMove = FindFarthestReusableChunk(currentPosition);
                if (chunkToMove == null)
                {
                    Debug.LogError("ChunkMover could not find a free Chunk to move.");
                    return;
                }

                var oldPosition = chunkToMove.localPosition;
                if (_chunksByPosition.TryGetValue(oldPosition, out var registeredChunk) && registeredChunk == chunkToMove)
                    _chunksByPosition.Remove(oldPosition);

                chunkToMove.localPosition = requiredPosition;
                _chunksByPosition[requiredPosition] = chunkToMove;
            }
        }

        private void RebuildChunksByPosition()
        {
            _chunksByPosition.Clear();

            foreach (var chunk in _chunksTransform)
                _chunksByPosition[chunk.localPosition] = chunk;
        }

        private Transform FindFarthestReusableChunk(Vector3 currentPosition)
        {
            Transform farthestChunk = null;
            var farthestSqrDistance = float.MinValue;

            foreach (var chunk in _chunksTransform)
            {
                if (chunk == _currentChunk || _requiredPositions.Contains(chunk.localPosition))
                    continue;

                var sqrDistance = (chunk.localPosition - currentPosition).sqrMagnitude;
                if (sqrDistance <= farthestSqrDistance)
                    continue;

                farthestSqrDistance = sqrDistance;
                farthestChunk = chunk;
            }

            return farthestChunk;
        }
    }
}
