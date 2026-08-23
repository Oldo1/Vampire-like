using UnityEngine;

namespace Assets.Scripts
{
    public class MovementRotator
    {
        public void RotateTowardsDirection(Transform target, Vector3 direction, float rotationSpeed)
        {
            if (direction == Vector3.zero)
                return;

            var from = target.rotation;
            var to = Quaternion.LookRotation(direction, Vector3.up);
            target.rotation = Quaternion.RotateTowards(from, to, rotationSpeed * Time.deltaTime);
        }
    }
}
