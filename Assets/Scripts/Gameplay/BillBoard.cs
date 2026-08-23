using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class BillBoard : MonoBehaviour
    {
        private GameObject _healthBar;
        private Camera _camera;

        [Inject]
        private void Construct(GameObject healthBar)
        {
            _healthBar = healthBar;
        }

        private void LateUpdate()
        {
            if (_camera == null)
                _camera = Camera.main;

            if (_camera == null)
                return;

            var cameraForward = _camera.transform.forward;
            _healthBar.transform.LookAt(cameraForward + transform.position);
        }
    }
}
