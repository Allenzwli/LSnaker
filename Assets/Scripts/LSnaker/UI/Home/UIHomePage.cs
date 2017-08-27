using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace LSnaker
{
    public class UIHomePage : UIPage
    {
        public Text userInfoText;

        protected override void OnOpen(object arg=null)
        {
            base.OnOpen();

            UpdateUserInfo();
        }

        private void UpdateUserInfo()
        {
            UserData userData = UserManager.Instance.MainUserData;
            userInfoText.text = userData.name + "(Lv." + userData.level + ")";
        }

        private void OpenModule(string name, string arg = null)
        {
            HomeModule module = ModuleManager.Instance.GetModule(ModuleDef.HomeModule) as HomeModule;
            if (module != null)
            {
                module.OpenModule(name, arg);
            }
        }


        public void OnUserInfoButtonClicked()
        {
            UIAPI.ShowMsgBox("重新登录","是否重新登录？","确定|取消",param=>{
                if((int)param==0)
                {
                    HomeModule module = ModuleManager.Instance.GetModule(ModuleDef.HomeModule) as HomeModule;
                    module.TryReLogin();
                }
            });
        }

        public void OnSettingButtonClicked()
        {
            OpenModule(ModuleDef.SettingModule);
        }

        public void OnDailyCheckButtonClicked()
        {
            OpenModule(ModuleDef.DailyCheckModule);
        }

        public void OnActivityButtonClicked()
        {
            OpenModule(ModuleDef.ActivityModule);
        }

        public void OnShopButtonClicked()
        {
            OpenModule(ModuleDef.ShopModule,"BuyCoin");
        }

        public void OnShareButtonClicked()
        {
            OpenModule(ModuleDef.ShareModule);
        }

        public void OnEndlessPVEButtonClicked()
        {
            OpenModule(ModuleDef.PVEModule,"Endless");
        }

		public void OnTimeLimitPVEButtonClicked()
		{
			OpenModule(ModuleDef.PVEModule, "Timelimit");
		}

        public void OnPVPButtonClicked()
        {
            OpenModule(ModuleDef.PVPModule);
        }
    }
}