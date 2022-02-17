using GameLogic;
using UnityEngine;
using Utilities.Events;

namespace Spawn
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private ItemPool _poolX;
        [SerializeField] private ItemPool _poolO;
        [SerializeField] private Transform _placeToSpawn;

        private bool _xSpawn = true;

        private void OnEnable()
        {
            EventsControllerXo.AddListener(EventsTypeXo.SpawnItem, Spawn);
            EventsControllerXo.AddListener(EventsTypeXo.ReStart, OnRestart);
        }

        private void OnDisable()
        {
            EventsControllerXo.RemoveListener(EventsTypeXo.SpawnItem, Spawn);
            EventsControllerXo.RemoveListener(EventsTypeXo.ReStart, OnRestart);
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            Item item;
            if (_xSpawn)
            {
                item = _poolX.GetItem();
                _xSpawn = false;
            }
            else
            {
                item = _poolO.GetItem();
                _xSpawn = true;
            }

            Transform transform1;
            (transform1 = item.transform).SetParent(_placeToSpawn);
            transform1.localPosition = Vector3.zero;
        }

        private void OnRestart()
        {
            _poolX.ResetPool();
            _poolO.ResetPool();
            _xSpawn = true;
            Spawn();
        }
    }
}