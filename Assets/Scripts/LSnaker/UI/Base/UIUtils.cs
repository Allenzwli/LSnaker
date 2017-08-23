using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LSnaker.UI
{
    public static class UIUtils
    {

        public static void SetActive(UIBehaviour ui,bool value)
        {
            if(ui!=null&&ui.gameObject!=null)
            {
                GameObjectUtils.SetActiveRecursively(ui.gameObject, value);
            }
        }

        public static void SetButtonText(Button btn,string text)
        {
            Text objText = btn.transform.GetComponentInChildren<Text>();
            if(objText!=null)
            {
                objText.text = text;
            }
        }

        public static string GetButtonText(Button btn)
        {
            Text objText = btn.transform.GetComponentInChildren<Text>();
            if(objText!=null)
            {
                return objText.text;
            }
            return null;
        }

        public static void SetChildText(UIBehaviour ui,string text)
        {
            Text objText = ui.transform.GetComponentInChildren<Text>();
            if(objText!=null)
            {
                objText.text = text;
            }
        }
    }
}
