using UnityEngine;
using System.Collections;

namespace LSnaker
{
    public static class UIRes
    {
        public static string UIResRoot = "ui/";

        public static GameObject LoadPrefab(string name)
        {
            LDebugger.Log("UIRes::LoadPrefab ",UIResRoot+name);
            GameObject asset = Resources.Load<GameObject>(UIResRoot + name);
            return asset;
        }

    }
}
