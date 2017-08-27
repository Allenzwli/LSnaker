using UnityEngine;
using UnityEngine.UI;

namespace LSnaker
{
	public class UILoginPage:UIPage
	{
		public InputField InputId;
		public InputField InputName;

		protected override void OnOpen(object arg)
		{
			base.OnOpen ();
			UserData userData = GameConfig.Config.MainUserData;
			InputName.text = userData.name;
			InputId.text = userData.id.ToString ();
		}

		public void OnLoginButtonClicked()
		{
			uint userId = 0;
			uint.TryParse (InputId.text, out userId);
			string userName = InputName.text.Trim ();
			if (userId == 0) 
			{
				userId = (uint)Random.Range (100000,999999);
			}
			LoginModule module = ModuleManager.Instance.GetModule (ModuleDef.LoginModule) as LoginModule;
			if (module != null) 
			{
				module.Login (userId,userName,"");
			}
		}
	}
}

