using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerInput
    {
        private readonly InputSystem _inputSystem;

        public PlayerInput(InputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _inputSystem.Player.Enable();
        }

        public void Disable()
        {
            _inputSystem.Player.Disable();
        }

        public void Enable()
        {
            _inputSystem.Player.Enable();
        }

        public Vector2 GetMoveInput()
        {
            var player = _inputSystem.Player;
            return player.Move.ReadValue<Vector2>().normalized;
        }
    }
}
