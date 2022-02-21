using System;
using DefaultNamespace;
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

        private bool _xSpawn;
        private GameSetup _gameSetup;


        private void OnEnable()
        {
            EventsControllerXo.AddListener(EventsTypeXo.SpawnItem, Spawn);
            EventsControllerXo.AddListener<GameSetup>(EventsTypeXo.SelectMark,SetGameSetup);
            EventsControllerXo.AddListener(EventsTypeXo.ReStart, OnRestart);
        }

        private void OnDisable()
        {
            EventsControllerXo.RemoveListener(EventsTypeXo.SpawnItem, Spawn);
            EventsControllerXo.RemoveListener(EventsTypeXo.ReStart, OnRestart);
            
            EventsControllerXo.RemoveListener<GameSetup>(EventsTypeXo.SelectMark,SetGameSetup);
        }
        
        private void Spawn()
        {
            Item item;

            if (_gameSetup.IsTwoPlayer)
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
        
        private void OnRestart()
        {
            _poolX.ResetPool();
            _poolO.ResetPool();
            _xSpawn = (_gameSetup.PlayerMark == Mark.X);
            Spawn();
        }

        private void SetGameSetup(GameSetup gameSetup)
        {
            _gameSetup = gameSetup;
            _xSpawn = _gameSetup.PlayerMark == Mark.X;
            Spawn();
        }
    }
}