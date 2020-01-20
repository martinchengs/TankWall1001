using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CC.EventBus
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class EventSubscribe : System.Attribute
    {
        private ThreadMode mode;
        private bool sticky;
        private EventCode type;
        public EventSubscribe(EventCode type, ThreadMode mode = ThreadMode.Main, bool sticky = false)
        {
            this.type = type;
            this.mode = mode;
            this.sticky = sticky;
        }

        public ThreadMode Mode
        {
            get { return this.mode; }
            private set { this.mode = value; }
        }
        public EventCode EventType
        {
            get { return this.type; }
            private set { this.type = value; }
        }
        public bool Sticky
        {
            get { return this.sticky; }
            private set { this.sticky = value; }
        }
    }



    public enum ThreadMode
    {
        Main,
        WorkThread
    }
}