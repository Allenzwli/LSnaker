using UnityEngine;
using System.Collections;

namespace LSnaker
{
    public static class UIAPI
    {
        /// <summary>
        /// Shows the message box.
        /// </summary>
        /// <returns>The message box.</returns>
        /// <param name="title">Title.</param>
        /// <param name="content">Content.</param>
        /// <param name="btnText">Button text spilt by "|". Eg: 确定|取消|关闭</param>
        public static UIWindow ShowMsgBox(string title, string content, string btnText, UIWindow.CloseEvent onCloseEvent=null)
        {
            UIMsgBox.UIMsgBoxArg arg = new UIMsgBox.UIMsgBoxArg();
            arg.content = content;
            arg.title = title;
            arg.btnText = btnText;
            UIWindow ui = UIManager.Instance.OpenWindow(UIDef.UIMsgBox, arg);
            if(ui!=null&&onCloseEvent!=null)
            {
                ui.OnCloseEvent += closeArg =>
                {
                    onCloseEvent(closeArg);
                };
            }
            return ui;
        }
    }
}
