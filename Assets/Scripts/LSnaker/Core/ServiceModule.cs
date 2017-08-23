using System;

namespace LSnaker.Core
{
    public abstract class ServiceModule<T> : Module where T : ServiceModule<T>, new()
    {
        private static T mInstance = default(T);

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new T();
                }
                return mInstance;
            }
        }

        protected void CheckSingleton()
        {
            if (mInstance == null)
            {
                throw new Exception("ServiceModule<" + typeof(T) + ">无法直接实例化，因为是单例");
            }
        }
    }

}