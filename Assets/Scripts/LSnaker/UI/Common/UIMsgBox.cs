using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LSnaker.UI.Base;

namespace LSnaker.UI.Common
{
    public class UIMsgBox : UIWindow
    {
		public class UIMsgBoxArg
		{
			public string title;

			//必选字段,Content必须有值
			public string content = "";

			public string btnText;
		}

        private UIMsgBoxArg mMsgBoxArg;
        public Text ContentText;
        public UIBehaviour ControlTitle;
        public Button[] buttons;

        protected override void OnOpen(object arg=null)
        {
            base.OnOpen(arg);
            mMsgBoxArg = arg as UIMsgBoxArg;
            ContentText.text = mMsgBoxArg.content;
            string[] btnTexts = mMsgBoxArg.btnText.Split('|');

            UIUtils.SetChildText(ControlTitle,mMsgBoxArg.title);
            UIUtils.SetActive(ControlTitle,!string.IsNullOrEmpty(mMsgBoxArg.title));

            float btnWidth = 200;
            float btnStartX = (1 - btnTexts.Length) * btnWidth / 2;

            for (int i = 0; i < buttons.Length;i++)
            {
                if(i<btnTexts.Length)
                {
                    UIUtils.SetButtonText(buttons[i],btnTexts[i]);
                    UIUtils.SetActive(buttons[i],true);
                    Vector3 pos = buttons[i].transform.localPosition;
                    pos.x = btnStartX + i * btnWidth;
                    buttons[i].transform.localPosition = pos;
                }
                else
                {
                    UIUtils.SetActive(buttons[i], false);
                }
            }

        }

        public void OnButtonClick(int btnIndex)
        {
            Button button = buttons[btnIndex];
            this.Close(btnIndex);
        }
    }
}
