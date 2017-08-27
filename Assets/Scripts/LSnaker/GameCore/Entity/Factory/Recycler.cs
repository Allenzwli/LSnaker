using UnityEngine;
using System.Collections.Generic;

namespace LSnaker
{
    public interface IRecyclableObject
    {
        string GetRecycleType();

        void Dispose();
    }

    public class Recycler
    {
        private static DictionaryEx<string, Stack<IRecyclableObject>> mIdleObjectsPool;

        public Recycler()
        {
            mIdleObjectsPool = new DictionaryEx<string, Stack<IRecyclableObject>>();
        }

        public void Release()
        {
            foreach(var pair in mIdleObjectsPool)
            {
                foreach(var obj in pair.Value)
                {
                    obj.Dispose();
                }
                pair.Value.Clear();
            }
        }

        public void Push(IRecyclableObject obj)
        {
            string type = obj.GetRecycleType();
            Stack<IRecyclableObject> stackIdleObject = mIdleObjectsPool[type];
            if(stackIdleObject==null)
            {
                stackIdleObject = new Stack<IRecyclableObject>();
                mIdleObjectsPool.Add(type,stackIdleObject);
            }
            stackIdleObject.Push(obj);
        }

        public IRecyclableObject Pop(string type)
        {
            Stack<IRecyclableObject> stackIdleObject = mIdleObjectsPool[type];
            if(stackIdleObject!=null&&stackIdleObject.Count>0)
            {
                return stackIdleObject.Pop();
            }
            return null;
        }

    }
}