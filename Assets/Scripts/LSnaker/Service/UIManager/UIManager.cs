using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LSnaker
{
    public class UIPageTrack
    {
        public string name;
        public string scene;
    }
    public class UIManager : ServiceModule<UIManager>
    {
        public static string MainScene = "Main";
        public static string MainPage = "UIMainPage";

        private Stack<UIPageTrack> mUIPageTrackStack;
        private UIPageTrack mCurrentPage;
        private Action<string> mOnSceneLoaded;
        private List<UIPanel> mLoadedPanelList;

        public UIManager()
        {
            mUIPageTrackStack = new Stack<UIPageTrack>();
            mLoadedPanelList = new List<UIPanel>();
        }

        public void Init(string uiResRoot)
        {
            CheckSingleton();
            UIRes.UIResRoot = uiResRoot;

            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (mOnSceneLoaded != null)
                {
                    mOnSceneLoaded(scene.name);
                }
            };
        }

        private T Load<T>(string name) where T : UIPanel
        {
            T ui = UIRoot.Find<T>(name);
            if (ui == null)
            {
                GameObject original = UIRes.LoadPrefab(name);
                if (original != null)
                {
                    GameObject go = GameObject.Instantiate(original) as GameObject;
                    ui = go.GetComponent<T>();
                    if (ui != null)
                    {
                        go.name = name;
                        UIRoot.AddChild(ui);
                    }
                    else
                    {
                        LDebugger.LogError(this.GetType().ToString(), "Load() Prefab没有增加对应组件：" + name);
                    }
                }
                else
                {
                    LDebugger.LogError(this.GetType().ToString(), "Load() Res Not Found: " + name);
                }
            }

            if (ui != null)
            {
                if (mLoadedPanelList.IndexOf(ui) < 0)
                {
                    mLoadedPanelList.Add(ui);
                }
            }
            return ui;
        }

        private T Open<T>(string name, object arg = null) where T : UIPanel
        {
            T ui = Load<T>(name);
            if (ui != null)
            {
                ui.Open(arg);
            }
            else
            {
                LDebugger.LogError(this.GetType().ToString(), "Open() Failed! Name:{0}", name);
            }
            return ui;
        }

        private void CloseAllLoadedPanels()
        {
            foreach (var loadedPanelItem in mLoadedPanelList)
            {
                if (loadedPanelItem.IsOpen)
                {
                    loadedPanelItem.Close();
                }
            }
        }

        public void EnterMainPage()
        {
            mUIPageTrackStack.Clear();
            OpenPageWorker(MainScene, MainPage, null);
        }

        #region UIPage管理
        public void OpenPage(string scene, string page, object arg = null)
        {
            LDebugger.Log(this.GetType().ToString(), "OpenPage() scene:{0},page:{1},args:{2}", scene, page, arg);
            if (mCurrentPage != null)
            {
                mUIPageTrackStack.Push(mCurrentPage);
            }
            OpenPageWorker(scene, page, arg);
        }

        public void OpenPage(string page, object arg = null)
        {
            OpenPage(MainScene, page, arg);
        }

        public void GoBackPage()
        {
            LDebugger.Log(this.GetType().ToString(), "GoBackPage()");
            if (mUIPageTrackStack.Count > 0)
            {
                var track = mUIPageTrackStack.Pop();
                OpenPageWorker(track.scene, track.name, null);
            }
            else if (mUIPageTrackStack.Count == 0)
            {
                EnterMainPage();
            }

        }

        private void OpenPageWorker(string scene, string page, object arg)
        {
            LDebugger.Log(this.GetType().ToString(), "OpenPageWork() scene:{0},page:{1},arg:{2} ", scene, page, arg);
            string oldScene = SceneManager.GetActiveScene().name;
            mCurrentPage = new UIPageTrack();
            mCurrentPage.scene = scene;
            mCurrentPage.name = page;
            CloseAllLoadedPanels();
            if (oldScene == scene)
            {
                Open<UIPage>(page, arg);
            }
            else
            {
                mOnSceneLoaded = (sceneName) =>
                {
                    if (sceneName.Equals(scene))
                    {
                        mOnSceneLoaded = null;
                        Open<UIPage>(page, arg);
                    }
                };
                SceneManager.LoadScene(scene);
            }
        }
        #endregion

        #region UIWindow管理
        public UIWindow OpenWindow(string name, object arg = null)
        {
            UIWindow ui = Open<UIWindow>(name, arg);
            return ui;
        }

        public T OpenWindow<T>(object arg = null) where T : UIWindow
        {
            T ui = Open<T>(typeof(T).Name, arg);
            return ui;
        }

        #endregion

        #region UIWidget管理
        public UIWidget OpenWidget(string name,object arg=null)
        {
            UIWidget ui = Open<UIWidget>(name, arg);
            return ui;
        }

        public T OpenWidget<T>(object arg=null) where T:UIWidget
        {
            T ui = Open<T>(typeof(T).Name, arg);
            return ui;
        }

        #endregion
    }
}
