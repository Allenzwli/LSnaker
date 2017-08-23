using UnityEngine;
using System.Collections;
using LSnaker.UI;

public class UITest : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		LDebugger.EnableLog = true;
		UIManager.Instance.Init("UI/");
		UIManager.MainPage = "UIPage1";
		UIManager.Instance.EnterMainPage();

	}
}
