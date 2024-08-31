using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace OMG
{
    public class GameFieldRawImageBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Camera raycastCamera;
        [SerializeField] private RectTransform clickCatcherRectTransform;
        [SerializeField] private CanvasGroup canvasGroup;

        private GameFieldInstanceProvider _gameFieldInstanceProvider;

        private bool isPointerDownSuccess;
        private Vector2 _pointerDownViewportCache;

        [Inject]
        public void Constructd(GameFieldInstanceProvider gameFieldInstanceProvider)
        {
            _gameFieldInstanceProvider = gameFieldInstanceProvider;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_gameFieldInstanceProvider is not { Instance: not null })
                return;

            isPointerDownSuccess = GetViewportClickInsideRect(clickCatcherRectTransform, eventData.position, raycastCamera, out _pointerDownViewportCache);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isPointerDownSuccess)
                return;

            if (GetViewportClickInsideRect(clickCatcherRectTransform, eventData.position, raycastCamera, out var pointerUpViewportCache))
            {
                canvasGroup.interactable = false;
                _gameFieldInstanceProvider.Instance.GameFieldCommandHandler.Move(_pointerDownViewportCache, pointerUpViewportCache);
            }

            canvasGroup.interactable = true;
            isPointerDownSuccess = false;
        }

        private bool GetViewportClickInsideRect(RectTransform rect, Vector2 clickPosition, Camera camera, out Vector2 viewport)
        {
            viewport = Vector3.zero;
            return true;
        }
    }
}
