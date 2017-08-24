using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSnaker.Module;

namespace LSnaker
{
    public class GameEntry : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            LDebugger.EnableLog = true;
            LDebugger.EnableSave = false;

            //TestModule testModule = new TestModule ();
            //testModule.Init ();
        }
    }
}
