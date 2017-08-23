using UnityEngine;
using System.Collections;

namespace LSnaker.UI
{
    
    public abstract class UIPanel :MonoBehaviour 
	{

        //UI框架逻辑
        public virtual void Open(object args=null)
        {
            LDebugger.Log(this.GetType().ToString(),"Open(),{0}",args);
        }

        public virtual void Close(object args = null)
        {
            LDebugger.Log(this.GetType().ToString(), "Close(),{0}",args);
        }
		
        public bool IsOpen
        {
            get
            {
                return this.gameObject.activeSelf;
            }
        }

        protected virtual void OnClose(object args = null)
        {
            LDebugger.Log(this.GetType().ToString(),"OnClose(),{0}",args);
        }

        //UI业务逻辑
        protected virtual void OnOpen(object args=null)
        {
            LDebugger.Log(this.GetType().ToString(),"OnOpen(),{0}",args);
        }
	}
}

