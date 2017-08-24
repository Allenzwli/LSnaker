using UnityEngine;
using System.Collections;
using LSnaker.UI.Base;


public class TestUIMsgBox : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        UIAPI.ShowMsgBox("testTitle", "testContent", "yes|no|cancel",
                         (args) =>
                         {
                            LDebugger.Log(this.GetType().ToString(),"testMsgBox"+args);
                         });


	}
}
