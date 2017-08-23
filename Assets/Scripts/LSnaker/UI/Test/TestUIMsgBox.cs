using UnityEngine;
using System.Collections;
using LSnaker.UI;


public class TestUIMsgBox : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
        var arg = new UIMsgBox.UIMsgBoxArg();
        arg.title = "testTitle";
        arg.content = "testContent";
        arg.btnText = "ok";
		
        UIManager.Instance.OpenWindow("common/UIMsgBox",arg);


	}
}
