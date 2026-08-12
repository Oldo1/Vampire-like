using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class CrystalsDetection
    {
        private readonly Transform _playerTransform;
        private readonly float _detectionRadius;
        private readonly LayerMask _crystalsLayerMask;
        private readonly Collider[] _crystalsCollidersBuffer;

        public CrystalsDetection(Transform playerTransform, float detectionRadius, LayerMask layerMask, int bufferLength)
        {
            _playerTransform = playerTransform;
            _detectionRadius = detectionRadius;
            _crystalsLayerMask = layerMask;
            _crystalsCollidersBuffer = new Collider[bufferLength];
        }

        public bool TryGetCrystalsNearbyCrystals(out IEnumerable<Crystal> crystals)
        {
            var playerPosition = _playerTransform.position;
            crystals = null;
            var overlapResult = Physics.OverlapSphereNonAlloc(playerPosition, _detectionRadius, _crystalsCollidersBuffer, (int)_crystalsLayerMask);

            if (overlapResult == 0)
                return false;

            crystals = _crystalsCollidersBuffer
                .TakeWhile(x => x != null)
                .Select(x => x.GetComponent<Crystal>());

            return true;
        }
    }
}
