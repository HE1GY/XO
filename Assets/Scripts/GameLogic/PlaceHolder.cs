using System;
using Drag_Drop;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Events;

namespace GameLogic
{
    [RequireComponent(typeof(DroppablePlace), typeof(Image))]
    public class PlaceHolder : MonoBehaviour
    {
        public Action MarkChanged;
        public Mark Mark { get; set; }

        [SerializeField] private Sprite _xSprite;
        [SerializeField] private Sprite _oSprite;

        private Image _image;
        private DroppablePlace _droppablePlace;

        private void Awake()
        {
            _droppablePlace = GetComponent<DroppablePlace>();
        }

        private void OnEnable()
        {
            EventsControllerXo.AddListener(EventsTypeXo.ReStart, OnRestart);
            _droppablePlace.ItemDrop += SetMark;
        }

        private void OnDisable()
        {
            EventsControllerXo.RemoveListener(EventsTypeXo.ReStart, OnRestart);
            _droppablePlace.ItemDrop -= SetMark;
        }

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        public void SetMark(Mark mark)
        {
            if (!_droppablePlace.HasImage)
            {
                Mark = mark;
                SetNewPicture();
                MarkChanged?.Invoke();
            }
        }

        private void SetNewPicture()
        {
            if (Mark == Mark.O)
            {
                _image.sprite = _oSprite;
                _droppablePlace.HasImage = true;
            }
            else if (Mark == Mark.X)
            {
                _image.sprite = _xSprite;
                _droppablePlace.HasImage = true;
            }
            else
            {
                _image.sprite = null;
                _droppablePlace.HasImage = false;
            }
        }

        private void OnRestart()
        {
            Mark = Mark.None;
            SetNewPicture();
        }
    }
}