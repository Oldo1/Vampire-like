using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    Transform _playerTransform;
    private Vector3 _playerPreviousPosition;

    [Inject]
    private void Construct(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _playerPreviousPosition = _playerTransform.position;
    }

    private void Update()
    {
        var offset = _playerTransform.position - _playerPreviousPosition;
        transform.position += offset;
        _playerPreviousPosition = _playerTransform.position;
    }
}
