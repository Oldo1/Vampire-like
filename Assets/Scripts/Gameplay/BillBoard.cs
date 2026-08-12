using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts
{
    public class BillBoard : MonoBehaviour
    {
        private GameObject _healthBar;
        private Camera _camera;

        [Inject]
        private void Construct(Camera camera, GameObject healthBar)
        {
            _camera = camera;
            _healthBar = healthBar;
        }

        private void LateUpdate()
        {
            var cameraForward = _camera.transform.forward;
            _healthBar.transform.LookAt(cameraForward + transform.position);
        }

    }
}
