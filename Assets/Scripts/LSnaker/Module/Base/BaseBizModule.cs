using System;
using System.Reflection;
using UnityEngine;

namespace LSnaker.Module.Base
{
    public abstract class BaseBizModule : Module
    {
        private string mName;

        private EventTable mEventTable;

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(mName))
                {
                    mName = this.GetType().Name;
                }
                return mName;
            }
        }

        public BaseBizModule()
        {

        }

        internal BaseBizModule(string name)
        {
            mName = name;
        }

        public virtual void Create(object args = null)
        {
            LDebugger.Log(GetType().ToString(), "Create() args={0}",args);
        }

        public override void Release()
        {
            if (mEventTable != null)
            {
                mEventTable.Clear();
                mEventTable = null;
            }
            base.Release();
        }

        internal void SetEventTable(EventTable eventTable)
        {
            mEventTable = eventTable;
        }

        public ModuleEvent Event(string eventName)
        {
            return GetEventTable().GetEvent(eventName);
        }


        protected EventTable GetEventTable()
        {
            if (mEventTable == null)
            {
                mEventTable = new EventTable();
            }
            return mEventTable;
        }

        internal void HandleMessage(string msg, object[] args)
        {
            LDebugger.Log(GetType().ToString(),"HandleMessage(),msg:{0},args{1}",msg,args);
            MethodInfo methodInfo = this.GetType().GetMethod(msg, BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo != null)
            {
                methodInfo.Invoke(this, BindingFlags.NonPublic, null, args, null);
            }
            else
            {
                OnModuleMessage(msg, args);
            }
        }

        protected virtual void OnModuleMessage(string msg, object[] args)
        {
            LDebugger.Log(GetType().ToString(), "OnModuleMessage(),msg:{0},args:{1}", msg, args);
        }

    }
}
