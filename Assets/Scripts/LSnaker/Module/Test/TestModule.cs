using System;
using UnityEngine;
using LSnaker.Module.Base;


    public class ModuleA : BaseBizModule
    {
        public override void Create(object args = null)
        {
            base.Create(args);
            ModuleManager.Instance.SendMessage("ModuleB", "MeesageFromA_1", 1, 2, 3);
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_2", "abc", 123);

            ModuleManager.Instance.Event("ModuleB", "OnModuleEventB").AddListener(OnModuleEventB);

            ModuleC.Instance.OnEvent.AddListener(OnModuleEventC);
            ModuleC.Instance.DoSomething();

            GlobalEvent.OnLoginEvent.AddListener(OnLogin);
        }

        private void OnModuleEventB(object args)
        {
            LDebugger.Log(this.GetType().ToString(), "OnModuleEventB() args:{0}", args);
        }

        private void OnModuleEventC(object args)
        {
            LDebugger.Log(this.GetType().ToString(), "OnModuleEventC() args:{0}", args);
        }

        private void OnLogin(bool args)
        {
            LDebugger.Log(this.GetType().ToString(), "OnLogin() args:{0}", args);
        }
    }

    public class ModuleB : BaseBizModule
    {
        public ModuleEvent OnModuleEventB
        {
            get
            {
                return Event("OnModuleEventB");
            }
        }

        public override void Create(object args = null)
        {
            base.Create(args);
            OnModuleEventB.Invoke("bbbb");
        }

        protected void MessageFromA_2(string args0, int args1)
        {
            LDebugger.Log(this.GetType().ToString(), "MessageFromA_2() args:{0},{1}", args0, args1);
        }

        protected override void OnModuleMessage(string msg, object[] args)
        {
            base.OnModuleMessage(msg, args);
            LDebugger.Log(this.GetType().ToString(), "OnModuleMessage() msg:{0},args:{1},{2},{3}", msg, args[0], args[1], args[2]);
        }

    }

    public class ModuleC : ServiceModule<ModuleC>
    {
        public ModuleEvent OnEvent = new ModuleEvent();

        public void Init()
        {

        }

        public void DoSomething()
        {
            OnEvent.Invoke(null);
        }

    }
    public class TestModule
    {
        public void Init()
        {
            ModuleC.Instance.Init();
            ModuleManager.Instance.Init("Snaker.Service.Core");
            ModuleManager.Instance.CreateModule("ModuleA");
            ModuleManager.Instance.CreateModule("ModuleB");
        }

    }
