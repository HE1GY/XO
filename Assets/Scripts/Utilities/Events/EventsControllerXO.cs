using System;
using System.Collections.Generic;

namespace Utilities.Events
{
    public static class EventsControllerXo
    {
        private static Dictionary<EventsTypeXo, List<Delegate>> events = new Dictionary<EventsTypeXo, List<Delegate>>();

        public static void AddListener(EventsTypeXo eventName, Action callback)
        {
            SetupEvent(eventName);

            if (events[eventName] == null)
                events[eventName] = new List<Delegate>();

            events[eventName].Add(callback);
        }

        public static void AddListener<T>(EventsTypeXo eventName, Action<T> callback)
        {
            SetupEvent(eventName);
            if (events[eventName] == null)
                events[eventName] = new List<Delegate>();

            events[eventName].Add(callback);
        }

        public static void AddListener<T, TU>(EventsTypeXo eventName, Action<T, TU> callback)
        {
            SetupEvent(eventName);
            if (events[eventName] == null)
                events[eventName] = new List<Delegate>();

            events[eventName].Add(callback);
        }

        public static void AddListener<T, TU, TR>(EventsTypeXo eventName, Action<T, TU, TR> callback)
        {
            SetupEvent(eventName);
            if (events[eventName] == null)
                events[eventName] = new List<Delegate>();

            events[eventName].Add(callback);
        }

        public static void RemoveListener(EventsTypeXo eventName, Action callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].Remove(callback);
                ListenerRemoved(eventName);
            }
        }

        public static void RemoveListener<T>(EventsTypeXo eventName, Action<T> callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].Add(callback);
                ListenerRemoved(eventName);
            }
        }

        public static void RemoveListener<T, TU>(EventsTypeXo eventName, Action<T, TU> callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].Add(callback);
                ListenerRemoved(eventName);
            }
        }

        public static void RemoveListener<T, TU, TR>(EventsTypeXo eventName, Action<T, TU, TR> callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].Add(callback);
                ListenerRemoved(eventName);
            }
        }

        public static void Broadcast(EventsTypeXo eventName)
        {
            if (CallCondition(eventName))
            {
                foreach (var item in events[eventName])
                {
                    ((Action)item)();
                }
            }
        }

        public static void Broadcast<T>(EventsTypeXo eventName, T param)
        {
            if (CallCondition(eventName))
            {
                foreach (var item in events[eventName])
                {
                    ((Action<T>)item)(param);
                }
            }
        }

        public static void Broadcast<T, TU>(EventsTypeXo eventName, T param, TU param2)
        {
            if (CallCondition(eventName))
            {
                foreach (var item in events[eventName])
                {
                    ((Action<T,TU>)item)(param,param2);
                }
            }
        }

        private static bool CallCondition(EventsTypeXo eventName)
        {
            return events.ContainsKey(eventName) && events[eventName] != null;
        }

        internal static void AddListener(EventsTypeXo checkLevel)
        {
            throw new NotImplementedException();
        }

        private static void ListenerRemoved(EventsTypeXo eventName)
        {
            if (events.ContainsKey(eventName) && events[eventName] == null)
                events.Remove(eventName);
        }

        private static void SetupEvent(EventsTypeXo eventName)
        {
            if (!events.ContainsKey(eventName))
                events.Add(eventName, null);
        }
    }
}