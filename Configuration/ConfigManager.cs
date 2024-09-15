using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TheGorillaWatch.Configuration
{
    class ConfigManager
    {
        public static ConfigEntry<bool> toggleButton;

        static ConfigFile config;

        public static void CreateConfig()
        {
            config = new ConfigFile(Path.Combine(Paths.ConfigPath, "GorillaWatch.cfg"), true);
            toggleButton = config.Bind("Settings:", "\nUse Trigger", false, "\nThis allows you to choose wether the mod toggle should be your right joystick down, or your left trigger. If 'Use Trigger' is false, you use your right joystick click to toggle, if it's true, you use your left trigger!");
        }
    }
}
