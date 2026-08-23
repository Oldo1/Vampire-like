using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class Joystick : MonoBehaviour
{
    [SerializeField] private OnScreenStick _onScreenStick;
    [SerializeField] private RectTransform _background;
    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
        var size = _onScreenStick.movementRange * 2;
        _background.sizeDelta = new Vector2(size, size);
    }

    private void OnDisable()
    {
        _onScreenStick.enabled = false;
        transform.position = _initialPosition;
    }

    private void OnEnable()
    {
        _onScreenStick.enabled = true;
    }
}
