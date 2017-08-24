using UnityEngine;
using System.Collections;

namespace LSnaker.UI.Base
{
    public static class UIRes
    {
        public static string UIResRoot = "ui/";

        public static GameObject LoadPrefab(string name)
        {
            GameObject asset = Resources.Load<GameObject>(UIResRoot + name);
            return asset;
        }

    }
}
