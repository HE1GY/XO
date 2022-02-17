using UnityEngine;
using UnityEngine.EventSystems;

namespace Drag_Drop
{
    public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public CanvasGroup _canvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransf;

        private void Awake()
        {
            _mainCanvas = GetComponentInParent<Canvas>();
            _rectTransf = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransf.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            transform.localPosition = Vector3.zero;
        }
    }
}