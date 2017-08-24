using System;

namespace LSnaker.Module.Base
{
    public class GlobalEvent
    {
        public static ModuleEvent<bool> OnLoginEvent = new ModuleEvent<bool>();

        public static ModuleEvent<bool> OnPayEvent = new ModuleEvent<bool>();
    }

}