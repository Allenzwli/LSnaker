using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSnaker.UI
{
    public class UIRoot:MonoBehaviour
    {
        public const string LOG_TAG="UIRoot";

        public static T Find<T>() where T:MonoBehaviour
        {
            string name = typeof(T).Name;
            GameObject go = Find(name);
            if(go!=null)
            {
                return go.GetComponent<T>();
            }
            return null;
        }

        public static T Find<T>(string name) where T:MonoBehaviour
        {
            GameObject obj = Find(name);
            if(obj!=null)
            {
                return obj.GetComponent<T>();
            }
            return null;
        }

        public static GameObject Find(string name) 
        {
            Transform subObjTf = null;
            GameObject root=FindUIRoot();
            if(root!=null)
            {
                subObjTf= root.transform.Find(name);
            }
            if (subObjTf!=null)
            {
                return subObjTf.gameObject;
            }
            return null;
        }

        public static GameObject FindUIRoot()
        {
            GameObject root = GameObject.Find("UIRoot");
            if(root!=null&&root.GetComponent<UIRoot>()!=null)
            {
                return root;
            }
            LDebugger.LogError(LOG_TAG,"FindUIRoot() UIRoot is not exist");
            return root;
        }

        public static void AddChild(UIPanel child)
        {
            GameObject root = FindUIRoot();
            if(root==null||child==null)
            {
                return;
            }
            child.transform.SetParent(root.transform, false);
        }



    }
}
