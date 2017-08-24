using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace LSnaker.Module.Base
{
    public class ModuleEvent : UnityEvent<object>
    {




    }

    public class ModuleEvent<T> : UnityEvent<T>
    {



    }

    public class EventTable
    {
        private Dictionary<string, ModuleEvent> mEventsMap;

        public ModuleEvent GetEvent(string eventName)
        {
            if (mEventsMap == null)
            {
                mEventsMap = new Dictionary<string, ModuleEvent>();
            }

            if (!mEventsMap.ContainsKey(eventName))
            {
                mEventsMap.Add(eventName, new ModuleEvent());
            }
            return mEventsMap[eventName];
        }

        public void Clear()
        {
            if (mEventsMap != null)
            {
                foreach (var itemEvent in mEventsMap)
                {
                    itemEvent.Value.RemoveAllListeners();
                }
                mEventsMap.Clear();
            }
        }

    }
}