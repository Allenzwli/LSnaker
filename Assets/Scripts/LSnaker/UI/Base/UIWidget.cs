using UnityEngine;
using System.Collections;

namespace LSnaker
{
    
	public class UIWidget : UIPanel
	{
        protected object mOpenArgs;
        
        public sealed override void Open(object args = null)
		{
			LDebugger.Log(this.GetType().ToString(), "Open(),{0}", args);
			mOpenArgs = args;
			if (!this.gameObject.activeSelf)
			{
				this.gameObject.SetActive(true);
			}
			OnOpen(args);
		}

        public sealed override void Close(object args = null)
		{
			LDebugger.Log(GetType().ToString(), "Close()");
			if (this.gameObject.activeSelf)
			{
				this.gameObject.SetActive(false);
			}
            OnClose(args);
		}
	}
}

