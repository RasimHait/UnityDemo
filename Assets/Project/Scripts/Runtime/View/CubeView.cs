using Core.Processors;
using Cysharp.Threading.Tasks;
using Project.Data;
using Project.Services;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using Zenject;

namespace Project.View
{
    public class CubeView : MonoBehaviour, IPoolableObject
    {
        public int Value { get; private set; } = 2;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _meshRenderer;
        [Inject] private readonly EventService _eventService;
        [Inject] private readonly ContentService _contentService;

        public void Launch(Vector3 direction, float force, Transform parent)
        {
            transform.SetParent(parent);
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        public float GetMomentum()
        {
            return _rigidbody.mass * _rigidbody.linearVelocity.magnitude;
        }

        public void OnAddToPool()
        {
            gameObject.SetActive(false);
        }

        public void OnPopFromPool()
        {
            UpdateView();
            gameObject.SetActive(true);
        }

        public void OnReturnToPool()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            Value = 2;
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CubeView cube))
            {
                if (GetMomentum() > cube.GetMomentum())
                {
                    _eventService.TriggerEvent(new EventData.Cube.Contact(this, cube, collision.contacts[0].point));
                }
            }
        }

        public async UniTask Upgrade()
        {
            Value *= 2;
            UpdateView();
            await UniTask.CompletedTask;
        }

        private void UpdateView()
        {
            var settings = _contentService.StaticData.CubeTextures[Value];
            _meshRenderer.material.color = settings.Color;
            _meshRenderer.material.mainTexture = settings.Texture;
        }
    }
}
