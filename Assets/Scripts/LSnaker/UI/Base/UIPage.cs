using System;
using UnityEngine;
using UnityEngine.UI;

namespace LSnaker
{
    public class UIPage : UIPanel
    {
        [SerializeField]
        private Button mGoBackButton;

        protected object mOpenArgs;

        private bool mIsOpenedOnce;

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
            if(mIsOpenedOnce)
            {
                OnClose();
            }
#endif
            if (mGoBackButton != null)
            {
                mGoBackButton.onClick.RemoveAllListeners();
            }
        }

        private void OnGoBackButtonClicked()
        {
            LDebugger.Log(this.GetType().ToString(),"OnGoBackButtonClicked()");
            //UIManager回退
            UIManager.Instance.GoBackPage();

        }

        public sealed override void Open(object args = null)
        {
            LDebugger.Log(this.GetType().ToString(),"Open(),{0}",args);
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
            LDebugger.Log(GetType().ToString(),"Close()");
            if(this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }
            OnClose(args);
        }

    }
}
