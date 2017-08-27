using UnityEngine;
using System.Collections.Generic;
using System;

namespace LSnaker
{
    public static class EntityFactory
    {
        public static bool EnableLog = false;
        private static string LOG_TAG = "EntityFactory";

        private static bool mIsInit = false;

        private static Recycler mRecycler;

        private static List<EntityObject> mObjectsList;

        public static void Init()
        {
            if(mIsInit)
            {
                return;
            }
            mIsInit = true;

            mObjectsList = new List<EntityObject>();
            mRecycler = new Recycler();
        }

        public static void Release()
        {
            mIsInit = false;

            for (int i = 0; i < mObjectsList.Count;i++)
            {
                mObjectsList[i].ReleaseInFactory();
                mObjectsList[i].Dispose();
            }
            mObjectsList.Clear();
            mRecycler.Release();
        }

        public static T InstanceEntity<T>() where T:EntityObject,new ()
        {
            EntityObject obj = null;
            bool useRecycler = true;

            Type type = typeof(T);
            obj = mRecycler.Pop(type.FullName) as EntityObject;
            if(obj==null)
            {
                useRecycler = false;
                obj = new T();
            }
            obj.InstanceInFactory();

            if(EnableLog&&LDebugger.EnableLog)
            {
                LDebugger.Log(LOG_TAG,"InstanceEntity() {0}:{1}, useRecycler:{2}",obj.GetType().Name,obj.GetHashCode(),useRecycler);
            }
            return (T)obj; 
        }

        public static void ReleaseEntity(EntityObject obj)
        {
            if(obj!=null)
            {
                if(EnableLog&& LDebugger.EnableLog)
                {
                    LDebugger.Log(LOG_TAG,"ReleaseEntity() {0}:{1}",obj.GetType().Name,obj.GetHashCode());
                }
                obj.ReleaseInFactory();
            }
        }

        //GameManager call
        public static void ClearReleasedObjects()
        {
            for (int i = mObjectsList.Count - 1; i >= 0;i--)
            {
                if(mObjectsList[i].IsReleased)
                {
                    EntityObject obj = mObjectsList[i];
                    mObjectsList.RemoveAt(i);

                    //加入对象池
                    mRecycler.Push(obj);
                }
            }
        }
    }
}