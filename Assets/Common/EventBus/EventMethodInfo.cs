using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
namespace CC.EventBus
{
    public class EventMethodInfo
    {

        public EventMethodInfo(UnityEngine.Object subscribe,MethodInfo method, EventCode type, string methodName, ThreadMode mode, bool sticky)
        {
            this.Subscribe = subscribe;
            this.Method = method;
            this.EventType = type;
            this.Mode = mode;
            this.Sticky = sticky;
            this.MethodName = methodName;
        }
        public UnityEngine.Object Subscribe { get; private set; }
        public MethodInfo Method { get; private set; }
        public EventCode EventType { get; private set; }
        public ThreadMode Mode { get; private set; }
        public bool Sticky { get; private set; }
        public string MethodName { get; private set; }
    }
}