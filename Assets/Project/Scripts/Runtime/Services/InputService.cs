using Core.Services;
using Project.Data;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Services
{
    public class InputService : BaseService
    {
        public float Horizontal => Input.GetAxis("Horizontal");
        public float Vertical => Input.GetAxis("Vertical");
        public IObservable<Vector2> ActiveDragOffset => _activeDragOffset;
        public IObservable<Vector3> ActiveDragDelta => _activeDragDelta;
        public IObservable<Vector3> ActiveDragBegin => _activeDragBegin;
        public IObservable<Vector3> ActiveDragEnd => _activeDragEnd;

        private readonly ReactiveProperty<Vector2> _activeDragOffset = new();
        private readonly ReactiveProperty<Vector3> _activeDragDelta = new();
        private readonly ReactiveCommand<Vector3> _activeDragBegin = new();
        private readonly ReactiveCommand<Vector3> _activeDragEnd = new();

        private Vector2 _activeDragOffsetInternal;
        private Vector2 _smoothedVelocity;
        private bool _isDragging;

        [Inject] private readonly EventService _eventService;
        [Inject] private readonly ContentService _contentService;

        protected override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleDragStart();
            }
            else if(Input.GetMouseButton(0))
            {
                HandleDrag();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                HandleDragStop();
            }
        }

        private void HandleDragStart()
        {
            if(_isDragging)
            {
                return;
            }

            _activeDragOffsetInternal = Vector2.zero;
            _activeDragOffset.Value = _activeDragOffsetInternal;
            _smoothedVelocity = Vector2.zero;
            _isDragging = true;

            _activeDragBegin.Execute(Input.mousePosition);
        }

        private void HandleDrag()
        {
            if (!_isDragging)
                return;

            var rawDelta = Input.mousePositionDelta;

            var normalizedDelta = new Vector2(
                rawDelta.x / Screen.width,
                rawDelta.y / Screen.height
            );

            var targetVelocity = normalizedDelta * _contentService.StaticData.DragSensitivity / Time.deltaTime;
            var smoothTime = _contentService.StaticData.DragSmoothTime;

            _smoothedVelocity = Vector2.Lerp(_smoothedVelocity,targetVelocity, 1f - Mathf.Exp(-Time.deltaTime / smoothTime));

            var finalDelta = _smoothedVelocity * Time.deltaTime;

            if (finalDelta.magnitude >= _contentService.StaticData.DragDeadZone)
            {
                _activeDragOffsetInternal += finalDelta;
                _activeDragOffset.Value = _activeDragOffsetInternal;
            }

            _activeDragDelta.Value = finalDelta;
        }

        private void HandleDragStop()
        {
            if(!_isDragging)
            {
                return;
            }

            _isDragging = false;
            _activeDragOffsetInternal = Vector2.zero;
            _activeDragOffset.Value = _activeDragOffsetInternal;
            _smoothedVelocity = Vector2.zero;

            _activeDragEnd.Execute(Input.mousePosition);
        }
    }
}
