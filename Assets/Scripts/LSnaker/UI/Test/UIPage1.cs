using UnityEngine;
using System.Collections;
using LSnaker.UI.Base;
using LSnaker.Service.UIManager;

public class UIPage1 :UIPage
{
	protected override void OnOpen(object arg = null)
	{
		base.OnOpen(arg);

	}

	public void OnBtnOpenPage2()
	{
		UIManager.Instance.OpenPage("UIPage2");
	}
}
