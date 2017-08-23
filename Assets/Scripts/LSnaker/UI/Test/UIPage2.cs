using UnityEngine;
using System.Collections;
using LSnaker.UI;

public class UIPage2 :UIPage
{
	public void OnBtnOpenWnd1()
	{
        UIManager.Instance.OpenWindow("UIWindow1").OnCloseEvent += OnWnd1Close;
	}

	private void OnWnd1Close(object arg)
	{
        LDebugger.Log(this.GetType().ToString(),"OnWnd1Close()");
	}

	public void OnBtnOpenWidget1()
	{
		UIManager.Instance.OpenWidget("UIWidget1");
	}



}
