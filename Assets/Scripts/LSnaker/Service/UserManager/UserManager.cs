using UnityEngine;
using System.Collections;

namespace LSnaker
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
