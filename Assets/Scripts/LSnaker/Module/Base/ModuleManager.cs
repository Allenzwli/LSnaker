using System;
using System.Collections.Generic;
using UnityEngine;

namespace LSnaker.Module.Base
{
    public class MessageObject
    {
        //消息目标模块名
        public string target;
        //消息内容
        public string msg;
        //消息参数
        public object[] args;
    }

    public class ModuleManager : ServiceModule<ModuleManager>
    {
        private Dictionary<string, BaseBizModule> mModulesMap;

        //预监听表
        private Dictionary<string, EventTable> mPreListenerEventsMap;

        //消息缓存（用于目标模块未加载完成时）
        private Dictionary<string, List<MessageObject>> mMessageCacheMap;

        private string mDomain;

        public ModuleManager()
        {
            mModulesMap = new Dictionary<string, BaseBizModule>();
            mMessageCacheMap = new Dictionary<string, List<MessageObject>>();
            mPreListenerEventsMap = new Dictionary<string, EventTable>();
        }

        public void Init(string domain = "")
        {
            CheckSingleton();
            mDomain = domain;
        }

        public T CreateModule<T>(object args = null) where T : BaseBizModule
        {
            return (T)CreateModule(typeof(T).Name, args);
        }

        public BaseBizModule CreateModule(string name, object args = null)
        {
            if (mModulesMap.ContainsKey(name))
            {
                return null;
            }

            BaseBizModule module = null;
            Type type = Type.GetType(mDomain + "." + name);
            if (type != null)
            {
                module = Activator.CreateInstance(type) as BaseBizModule;
            }
            else
            {
                module = new LuaModule(name);
            }
            mModulesMap.Add(name, module);

            //
            if (mPreListenerEventsMap.ContainsKey(name))
            {
                EventTable eventTable = mPreListenerEventsMap[name];
                mPreListenerEventsMap.Remove(name);
                module.SetEventTable(eventTable);
            }
            module.Create(args);

            //
            if (mMessageCacheMap.ContainsKey(name))
            {
                List<MessageObject> list = mMessageCacheMap[name];
                foreach (var messageObject in list)
                {
                    module.HandleMessage(messageObject.msg, messageObject.args);
                }
                mMessageCacheMap.Remove(name);
            }
            return module;
        }

        public void ReleaseModule(BaseBizModule module)
        {
            if (module != null)
            {
                if (mModulesMap.ContainsKey(module.Name))
                {
                    mModulesMap.Remove(module.Name);
                    module.Release();
                }
                else
                {

                }
            }
        }

        public void ReleaseAll()
        {
            foreach (var eventItem in mPreListenerEventsMap)
            {
                eventItem.Value.Clear();
            }
            mPreListenerEventsMap.Clear();
            mMessageCacheMap.Clear();
            foreach (var module in mModulesMap)
            {
                module.Value.Release();
            }
            mModulesMap.Clear();
        }

        public T GetModule<T>() where T : BaseBizModule
        {
            return (T)GetModule(typeof(T).Name);
        }

        public BaseBizModule GetModule(string name)
        {
            if (mModulesMap.ContainsKey(name))
            {
                return mModulesMap[name];
            }
            return null;
        }

        #region message
        public void SendMessage(string target, string msg, params object[] args)
        {
            BaseBizModule module = GetModule(target);
            if (module != null)
            {
                module.HandleMessage(msg, args);
            }
            else
            {
                List<MessageObject> list = GetCacheMessagelist(target);
                MessageObject messageObject = new MessageObject();
                messageObject.target = target;
                messageObject.msg = msg;
                messageObject.args = args;
                list.Add(messageObject);
            }
        }

        private List<MessageObject> GetCacheMessagelist(string target)
        {
            List<MessageObject> list = null;
            if (!mMessageCacheMap.ContainsKey(target))
            {
                list = new List<MessageObject>();
                mMessageCacheMap.Add(target, list);
            }
            else
            {
                list = mMessageCacheMap[target];
            }
            return list;
        }
        #endregion

        #region event
        public ModuleEvent Event(string target, string type)
        {
            ModuleEvent moduleEvent = null;
            BaseBizModule module = GetModule(target);
            if (module != null)
            {
                moduleEvent = module.Event(type);
            }
            else
            {
                EventTable table = GetPreListenEventTable(target);
                moduleEvent = table.GetEvent(type);
            }
            return moduleEvent;
        }

        private EventTable GetPreListenEventTable(string target)
        {
            EventTable table = null;
            if (!mPreListenerEventsMap.ContainsKey(target))
            {
                table = new EventTable();
                mPreListenerEventsMap.Add(target, table);
            }
            else
            {
                table = mPreListenerEventsMap[target];
            }
            return table;
        }
        #endregion
    }

}