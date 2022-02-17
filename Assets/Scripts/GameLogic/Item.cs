using Drag_Drop;
using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(DraggableObject))]
    public class Item : MonoBehaviour
    {
        public Mark Mark { get; private set; }

        [SerializeField] private Mark _mark;

        private void Awake()
        {
            Mark = _mark;
        }
    }
}