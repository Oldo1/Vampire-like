using Assets.Scripts;
using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    public const string RotationSpeedId = "PlayerRotationSpeed";

    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private MovementRotator _movementRotator;
    private Camera _camera;
    private float _speed;
    private float _rotationSpeed;

    [Inject]
    private void Construct(CharacterController characterController, float speed, Camera camera, PlayerInput playerInput,
        MovementRotator movementRotator, [Inject(Id = RotationSpeedId)] float rotationSpeed)
    {
        _characterController = characterController;
        _speed = speed;
        _camera = camera;
        _playerInput = playerInput;
        _movementRotator = movementRotator;
        _rotationSpeed = rotationSpeed;
    }

    private void Update()
    {
        var direction = GetMoveDirection();
        if (direction != Vector3.zero)
        {
            var velocity = _speed * direction;
            _characterController.Move(velocity * Time.deltaTime);
            _movementRotator.RotateTowardsMovement(transform, direction, _rotationSpeed);
        }
    }

    private Vector3 GetMoveDirection()
    {
        var inputValue = _playerInput.GetMoveInput();

        var forward = _camera.transform.forward;
        forward.y = 0;
        forward.Normalize();
        forward *= inputValue.y;

        var right = _camera.transform.right;
        right.y = 0;
        right.Normalize();
        right *= inputValue.x;

        return (forward + right).normalized;
    }
}
