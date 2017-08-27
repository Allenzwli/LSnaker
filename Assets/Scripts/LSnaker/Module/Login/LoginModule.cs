using UnityEngine;
using System.Collections;

namespace LSnaker
{
	public class LoginModule:BaseBizModule
    {

		public void Login(uint id,string name,string pwd)
		{
			UserData userData = new UserData ();
			userData.id = id;
			userData.name = name;
			userData.defaultSnakeId = 1;

            //TODO:后台登录逻辑
            if(true)
            {
                OnLoginSuccess(userData);    
            }
    	}

		private void OnLoginSuccess(UserData userdata)
		{
			UserManager.Instance.UpdateMainUserData (userdata);

            //保存配置文件
			GameConfig.Config.MainUserData = UserManager.Instance.MainUserData;
			GameConfig.Save ();

            //发送全局事件
			GlobalEvent.OnLoginEvent.Invoke (true);

            //UI跳转
			UIManager.Instance.EnterMainPage ();
		}
	}
}
