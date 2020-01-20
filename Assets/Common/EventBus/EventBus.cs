using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace CC.EventBus
{
    public class EventBus
    {
        private static EventBus defaultInstance;
        private Dictionary<Type, List<EventMethodInfo>> subscriptionsByEventType = new Dictionary<Type, List<EventMethodInfo>>();
        private static readonly object locked = new System.Object();
        public static EventBus GetDefault()
        {
            if (defaultInstance == null)
            {
                lock (locked)
                {
                    if (defaultInstance == null)
                    {
                        defaultInstance = new EventBus();
                    }
                }
            }
            return defaultInstance;

        }

        public bool isRegisted(UnityEngine.Object subscriber)
        {
            Type type = subscriber.GetType();
            return subscriptionsByEventType.ContainsKey(type);
        }

        public void Register(UnityEngine.Object subscriber)
        {
            Type type = subscriber.GetType();
            MethodInfo[] methods = type.GetMethods();
            if (methods == null || methods.Length == 0)
            {
                throw new Exception("No Method Be Defined");
            }
            int count = 0;
            foreach (MethodInfo method in methods)
            {
                EventSubscribe attrs = method.GetCustomAttribute(typeof(EventSubscribe)) as EventSubscribe;
                if (attrs != null)
                {
                    count++;
                    var info = new EventMethodInfo(subscriber, method, attrs.EventType, method.Name, attrs.Mode, attrs.Sticky);
                    if (!subscriptionsByEventType.ContainsKey(type))
                    {
                        subscriptionsByEventType.Add(type, new List<EventMethodInfo>());
                    }
                    subscriptionsByEventType[type].Add(info);
                }
                if (count == 0)
                {
                    throw new Exception("No Method Be Defined With EventSubscribe Attribute");
                }
            }
        }

        public void UnRegister(UnityEngine.Object subscriber)
        {
            Type type = subscriber.GetType();
            if (subscriptionsByEventType.ContainsKey(type))
            {
                List<EventMethodInfo> list = subscriptionsByEventType[type];
                list.Clear();
                subscriptionsByEventType.Remove(type);
            }
        }

        public void postEvent(EventCode type, params object[] data)
        {
            var values = subscriptionsByEventType.Values;
            bool hasInvoked = false;
            foreach (var item in values)
            {
                foreach (var info in item)
                {
                    if (info.EventType == type)
                    {
                        hasInvoked = true;
                        info.Method.Invoke(info.Subscribe, data);
                        return;
                    }
                }
            }
            if (!hasInvoked)
            {
                throw new Exception($"The EventType({type}) is Not Subscribe");
            }
        }
    }
}

