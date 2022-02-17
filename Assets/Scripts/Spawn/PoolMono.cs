using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Spawn
{
    // не я придумав)
    public class PoolMono<T> where T : MonoBehaviour
    {
        public T Prefab { get; }
        public bool AutoExpand { get; set; }
        public Transform Container { get; }

        private List<T> _pool;

        public PoolMono(T prefab, int count)
        {
            Prefab = prefab;
            Container = null;

            GreatePool(count);
        }

        public PoolMono(T prefab, int count, Transform container)
        {
            Prefab = prefab;
            Container = container;

            GreatePool(count);
        }

        private void GreatePool(int count)
        {
            _pool = new List<T>();
            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(Prefab, Container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        private bool HasFreeElement(out T element)
        {
            foreach (var monoBehaviour in _pool)
            {
                if (!monoBehaviour.gameObject.activeInHierarchy)
                {
                    element = monoBehaviour;
                    element.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
            {
                return element;
            }

            if (AutoExpand)
            {
                return CreateObject(true);
            }

            throw new UnityException($"There is no free element in pool of type{typeof(Type)}");
        }

        public void PoolReset()
        {
            foreach (var element in _pool)
            {
                element.gameObject.SetActive(false);
                element.gameObject.transform.SetParent(Container);
            }
        }
    }
}