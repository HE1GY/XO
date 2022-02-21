using System;
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
        private bool _isXstartMark=true;
        private bool _2PlayerPlaying;
        

        private void OnEnable()
        {
            EventsControllerXo.AddListener<bool>(EventsTypeXo.SelectMode,OnSelectMode);
            EventsControllerXo.AddListener(EventsTypeXo.SpawnItem, Spawn);
            EventsControllerXo.AddListener(EventsTypeXo.ReStart, OnRestart);
            EventsControllerXo.AddListener<bool>(EventsTypeXo.SelectMark,OnSelectedMark);
        }

        private void OnDisable()
        {
            EventsControllerXo.RemoveListener<bool>(EventsTypeXo.SelectMode,OnSelectMode);
            EventsControllerXo.RemoveListener(EventsTypeXo.SpawnItem, Spawn);
            EventsControllerXo.RemoveListener(EventsTypeXo.ReStart, OnRestart);
            EventsControllerXo.RemoveListener<bool>(EventsTypeXo.SelectMark,OnSelectedMark);
        }
        


        private void Spawn()
        {
            Item item;

            if (_2PlayerPlaying)
            {
                item = PlayersSpawn();
            }
            else
            {
                item =PlayerSpawn();
            }
            
            Transform transform1;
            (transform1 = item.transform).SetParent(_placeToSpawn);
            transform1.localPosition = Vector3.zero;
        }

        private Item PlayersSpawn()
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
            return item;
        }

        private Item PlayerSpawn()
        {
            if (_xSpawn)
            {
                return _poolX.GetItem();
            }
            else
            {
                return _poolO.GetItem();
            }
        }
        
        private void OnSelectMode(bool playersPlaying)
        {
            _2PlayerPlaying = playersPlaying;
            if (_2PlayerPlaying)
            {
                Spawn();
            }
        }

        private void OnRestart()
        {
            _poolX.ResetPool();
            _poolO.ResetPool();
            _xSpawn = _isXstartMark;
            Spawn();
        }

        private void OnSelectedMark(bool isXSelected)
        {
            _xSpawn = isXSelected;
            if (isXSelected)
            {
                _isXstartMark = true;
            }
            else
            {
                _isXstartMark = false;
            }
            Spawn();
        }
    }
}