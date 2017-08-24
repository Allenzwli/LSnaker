using UnityEngine;
using System.Collections;
using LSnaker.Service.UserManager.Data;
using LSnaker.Module.Base;

namespace LSnaker.Service.UserManager
{
    public class UserManager : ServiceModule<UserManager>
    {
        private UserData mMainUserData;

        public UserData MainUserData
        {
            get
            {
                return mMainUserData;
            }
        }

        public void Init()
        {
            CheckSingleton();
        }

        public void UpdateMainUserData(UserData data)
        {
            mMainUserData = data;
        }
    }
}
