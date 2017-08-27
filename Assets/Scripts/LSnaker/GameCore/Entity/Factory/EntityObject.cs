using System;
using UnityEngine;

namespace LSnaker
{
    public abstract class EntityObject:IRecyclableObject
    {
        private bool mIsReleased = false;

        public bool IsReleased
        {
            get
            {
                return mIsReleased;
            }
        }

        internal void InstanceInFactory()
        {
            mIsReleased = false;
        }

        internal void ReleaseInFactory()
        {
            if (!mIsReleased)
            {
                Release();
                mIsReleased = true;
            }
        }

        protected abstract void Release();

        //逻辑坐标
        public virtual Vector3 Position()
        {
            return Vector3.zero;
        }

        //回收类型
        public string GetRecycleType()
        {
            return this.GetType().FullName;
        }

        public void Dispose()
        {
            //By GC
            //Do Nothing
        }
    }
}
