using System;
using UnityEngine;

namespace LSnaker.Module.Base
{
    public abstract class Module
    {
        public virtual void Release()
        {
            LDebugger.Log(this.GetType().ToString(), "Release");
        }
    }
}

