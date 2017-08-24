using System;

namespace LSnaker.Module.Base
{
    public class LuaModule : BaseBizModule
    {
        private object mArgs = null;

        internal LuaModule(string name) : base(name)
        {

        }

        public override void Create(object args = null)
        {
            base.Create(args);
            mArgs = args;

        }

        public override void Release()
        {
            base.Release();
        }
    }

}