using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entities
{
    public class Chunk : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Chunk>
        {
        }
    }
}
