using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Projectile : MonoBehaviour
    {
        public abstract void Launch(Transform target, float damage, float speed);
    }
}
