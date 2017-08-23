using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LSnaker.UI
{

	public class UIWindow : UIPanel
	{
        public delegate void CloseEvent(object args=null);

        [SerializeField]
        private Button mGoBackButton;

        public event CloseEvent OnCloseEvent;

        protected object mOpenArgs;

        private bool mIsOpenedOnce;

		private void OnGoBackButtonClicked()
		{
			LDebugger.Log(this.GetType().ToString(), "OnGoBackButtonClicked()");
            Close();

		}

		protected void OnEnable()
		{
			LDebugger.Log(this.GetType().ToString(), "OnEnable");
			if (mGoBackButton != null)
			{
                mGoBackButton.onClick.AddListener(OnGoBackButtonClicked);
			}
#if UNITY_EDITOR
			if (mIsOpenedOnce)
			{
				OnOpen(mOpenArgs);
			}
#endif
		}

		protected void OnDisable()
		{
			LDebugger.Log(this.GetType().ToString(), "OnDisable()");
#if UNITY_EDITOR
			if (mIsOpenedOnce)
			{
				OnClose();
                if(OnCloseEvent!=null)
                {
                    OnCloseEvent();
                    OnCloseEvent = null;
                }
			}
#endif
			if (mGoBackButton != null)
			{
				mGoBackButton.onClick.RemoveAllListeners();
			}
		}

		public sealed override void Open(object args = null)
		{
			LDebugger.Log(this.GetType().ToString(), "Open(),args= "+args);
			mOpenArgs = args;
			mIsOpenedOnce = false;
			if (!this.gameObject.activeSelf)
			{
				this.gameObject.SetActive(true);
			}
			OnOpen(args);
			mIsOpenedOnce = true;
		}

		public sealed override void Close(object args = null)
		{
            LDebugger.Log(this.GetType().ToString(), "Close() args= "+args);
			if (this.gameObject.activeSelf)
			{
				this.gameObject.SetActive(false);
			}
			OnClose(args);
			if (OnCloseEvent != null)
			{
                OnCloseEvent(args);
				OnCloseEvent = null;
			}
		}
	}
}

