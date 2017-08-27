using UnityEngine;
using System;

namespace LSnaker
{
    public abstract class ViewObject : MonoBehaviour,IRecyclableObject
    {
        private string mRecycleType;

        internal void CreateInFactory(EntityObject entity, string recycleType)
        {
            mRecycleType = recycleType;
            Create(entity);
        }

        protected abstract void Create(EntityObject entity);

        internal void ReleaseInFactory()
        {
            Release();
        }

        protected abstract void Release();

        public string GetRecycleType()
        {
            return mRecycleType;
        }

        public void Dispose()
        {
            try
            {
                GameObject.Destroy(this.gameObject);
            }
            catch(Exception e)
            {
                
            }
        }
    }
}