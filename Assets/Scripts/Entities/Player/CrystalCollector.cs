using Assets.Scripts.ObjectPool;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerScripts
{
    public class CrystalCollector : MonoBehaviour
    {
        private CrystalObjectPool _crystallObjectPool;
        public event Action<Crystal> OnCollect;

        [Inject]
        public void Construct(CrystalObjectPool crystallObjectPool)
        {
            _crystallObjectPool = crystallObjectPool;
        }    

        private void OnTriggerEnter(Collider other)
        {
            if (enabled == false) return;

            var crystal = other.GetComponentInParent<Crystal>();
            if (crystal != null)
            {
                _crystallObjectPool.Release(crystal);
                OnCollect?.Invoke(crystal);
            }
        }
    }
}
