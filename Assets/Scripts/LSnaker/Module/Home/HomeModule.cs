using UnityEngine;
using System.Collections;

namespace LSnaker
{
    public class HomeModule :BaseBizModule
    {
        public void TryReLogin()
        {
            //TODO:是否可以重新登录
            if(true)
            {
                UIManager.Instance.OpenPage(UIDef.UILoginPage);    
            }
        }


        public void OpenModule(string name,string arg)
        {
            UIAPI.ShowMsgBox(name,"模块正在开发中...","确定"); 
        }
           
    }
}
