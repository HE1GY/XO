using GameLogic;
using UnityEngine;

namespace Spawn
{
    public class ItemPool : MonoBehaviour
    {
        [SerializeField] private Item _item;

        [SerializeField] private int _count;
        [SerializeField] private bool _autoExpand;

        private PoolMono<Item> _pool;

        private void Awake()
        {
            _pool = new PoolMono<Item>(_item, _count, this.transform);
            _pool.AutoExpand = _autoExpand;
        }

        public Item GetItem()
        {
            Item draggableObject = _pool.GetFreeElement();
            return draggableObject;
        }

        public void ResetPool()
        {
            _pool.PoolReset();
        }
    }
}