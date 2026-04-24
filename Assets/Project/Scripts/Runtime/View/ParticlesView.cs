using Core.Processors;
using UnityEngine;

namespace Project
{
    public class ParticlesView : MonoBehaviour, IPoolableObject
    {
        [SerializeField] private ParticleSystem _system;

        public void OnAddToPool()
        {
            gameObject.SetActive(false);
        }

        public void OnPopFromPool()
        {
            gameObject.SetActive(true);
            _system.Play();
        }

        public void OnReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
