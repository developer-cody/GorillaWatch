﻿using System.IO;
using BepInEx;
using BepInEx.Configuration;

namespace TheGorillaWatch.Behaviors
{
    class ConfigManager
    {
        private static ConfigFile config;

        public static ConfigEntry<bool> toggleModButton, toggleWatchButton, toggleableWatch, platformConfig;
        public static ConfigEntry<float> bigMonkersSize, smallMonkersSize;

        public static void CreateConfig()
        {
            config = new ConfigFile(Path.Combine(Paths.ConfigPath, "GorillaWatch.cfg"), true);

            toggleModButton = config.Bind("Settings", "Use Left Trigger for Mod Toggle", false, "Choose whether to use your left joystick down or your left trigger to toggle the mod. If false, you will use your right joystick click to toggle. If true, you will use your left trigger.");

            toggleWatchButton = config.Bind("Settings", "Use Right Trigger for Watch Toggle", false, "Choose whether to use your right joystick down or your right trigger to toggle the watch. If false, you will use your right joystick click to toggle. If true, you will use your right trigger.");
            toggleableWatch = config.Bind("Settings", "Do You Want Your Watch to be Toggleable?", true, "Choose whether you want your watch to be toggleable with either your right joystick or trigger. If false, you cannot toggle the watch. If true, you can toggle it with either your right joystick or trigger!");

            bigMonkersSize = config.Bind("Settings", "How Big Do You Want BigMonkers to Be?", 2f, "Choose how big you want the scale to be when BigMonkers is enabled.");
            smallMonkersSize = config.Bind("Settings", "How Small Do You Want SmallMonkers to Be?", 0.1f, "Choose how small you want the scale to be when SmallMonkers is enabled.");

            if (bigMonkersSize.Value <= 1f) bigMonkersSize.Value = 2f;

            if (smallMonkersSize.Value >= 1f) smallMonkersSize.Value = 0.1f;
            else if (smallMonkersSize.Value < 0.1f) smallMonkersSize.Value = 0.1f;
        }
    }
}