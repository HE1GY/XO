using System;
using GameLogic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities.Events;

namespace Drag_Drop
{
    public class DroppablePlace : MonoBehaviour, IDropHandler
    {
        public Action<Mark> ItemDrop;

        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggableObj = eventData.pointerDrag;
            Mark itemMark = GetMark(draggableObj);

            ItemDrop?.Invoke(itemMark);
            draggableObj.SetActive(false);

            EventsControllerXo.Broadcast(EventsTypeXo.SpawnItem);
        }

        private Mark GetMark(GameObject draggableObj)
        {
            return draggableObj.GetComponent<Item>().Mark;
        }
    }
}