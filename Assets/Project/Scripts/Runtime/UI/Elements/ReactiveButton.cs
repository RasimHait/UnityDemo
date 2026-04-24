using DG.Tweening;
using Project.Data;
using Project.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI
{
    public class ReactiveButton : Button
    {
        [field: SerializeField] public string Tag { get; set; }
        public Vector3 InitialLocalPosition { get; private set; }

        private Vector3? _cachedScale;
        private bool _actionInProgress;

        [Inject] private readonly EventService _eventService;

        private new void Awake()
        {
            base.Awake();

            InitialLocalPosition = transform.localPosition;
            onClick.RemoveAllListeners();
            onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if(_actionInProgress)
            {
                return;
            }

            _actionInProgress = true;

            if (_cachedScale.HasValue)
            {
                transform.localScale = _cachedScale.Value;
            }

            _cachedScale = transform.localScale;

            var pushFactor = 0.8f;
            var stepDuration = 0.2f;

            _eventService.TriggerEvent(new EventData.UI.TapOnButtonBefore(Tag));

            var seq = DOTween.Sequence();
            seq.SetUpdate(true);
            seq.Append(transform.DOScale(pushFactor, stepDuration).SetEase(Ease.OutQuad));
            seq.Append(transform.DOScale(_cachedScale.Value, stepDuration).SetEase(Ease.OutBounce));
            seq.OnComplete(() =>
            {
                if (gameObject.activeInHierarchy)
                {
                    _eventService.TriggerEvent(new EventData.UI.TapOnButton(Tag));
                    _actionInProgress = false;
                }
            });
        }
    }
}
