using UnityEngine;
using System.Collections;

namespace LSnaker
{
    public static class UIRes
    {
        public static string UIResRoot = "ui/";

        public static GameObject LoadPrefab(string prefabName)
        {
            LDebugger.Log("UIRes::LoadPrefab ",UIResRoot+prefabName);
            GameObject asset = Resources.Load<GameObject>(UIResRoot + prefabName);
            return asset;
        }
    }
}
