using UnityEngine;
using System.Collections;
using LSnaker.Module.Base;
using LSnaker.Service.UserManager.Data;
using LSnaker.Service.UserManager;
using LSnaker.Service.UIManager;

namespace LSnaker.Module.Login
{
	public class LoginModule:BaseBizModule
    {

		public void Login(uint id,string name,string pwd)
		{
			UserData userData = new UserData ();
			userData.id = id;
			userData.name = name;
			userData.defaultSnakeId = 1;
			OnLoginSuccess (userData);
    	}

		private void OnLoginSuccess(UserData userdata)
		{
			UserManager.Instance.UpdateMainUserData (userdata);
			GameConfig.Config.MainUserData = UserManager.Instance.MainUserData;
			GameConfig.Save ();
			GlobalEvent.OnLoginEvent.Invoke (true);
			UIManager.Instance.EnterMainPage ();
		}
	}
}
