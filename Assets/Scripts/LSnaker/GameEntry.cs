using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSnaker;

namespace LSnaker
{
    public class GameEntry : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            LDebugger.EnableLog = true;
            LDebugger.EnableSave = false;

            GameConfig.Init();

            InitService();
            InitBusiness();

            UIManager.Instance.OpenPage(UIDef.UILoginPage);
        }

        private void InitService()
        {
            ModuleManager.Instance.Init("LSnaker");

			UIManager.Instance.Init("ui/");
            UIManager.MainPage = UIDef.UIHomePage;
            UIManager.MainScene = "Main";

            UserManager.Instance.Init();
        }

        private void InitBusiness()
        {
            ModuleManager.Instance.CreateModule(ModuleDef.LoginModule);
            ModuleManager.Instance.CreateModule(ModuleDef.HomeModule);
        }

    }
}
