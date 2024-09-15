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
            toggleButton = config.Bind("Settings", "Use Trigger", false, "Whether the mod should use the trigger button on the left controller or the joystick button on the right controller to toggle mods");
        }
    }
}
