﻿using System;

namespace LSnaker
{
    public class GlobalEvent
    {
        public static ModuleEvent<bool> OnLoginEvent = new ModuleEvent<bool>();

        public static ModuleEvent<bool> OnPayEvent = new ModuleEvent<bool>();
    }

}