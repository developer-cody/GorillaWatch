using BepInEx;
using BepInEx.Configuration;
using System.IO;

namespace TheGorillaWatch.Configuration
{
    class ConfigManager
    {
        public static ConfigEntry<bool> toggleModButton;
        
        public static ConfigEntry<bool> toggleWatchButton;

        public static ConfigEntry<bool> toggleableWatch;

        static ConfigFile config;

        public static void CreateConfig()
        {
            config = new ConfigFile(Path.Combine(Paths.ConfigPath, "GorillaWatch.cfg"), true);

            toggleModButton = config.Bind("Settings", "Use Left Trigger For Mod Toggle", false,
                "This allows you to choose whether the mod toggle should be your left joystick down, or your left trigger. " +
                "If it's false, you use your right joystick click to toggle. If it's true, you use your left trigger.");

            toggleWatchButton = config.Bind("Settings", "Use Right Trigger For Watch Toggle", false,
                "This allows you to choose whether the watch toggle should be your right joystick down, or your right trigger. " +
                "If it's false, you use your right joystick click to toggle. If it's true, you use your right trigger.");
            
            toggleableWatch = config.Bind("Settings", "Do you want your watch to be toggleable?", true,
                "This allows you to choose wether you want your watch to be toggleable with either your right joystick or trigger!" +
                "If it's false, you can't toggle the watch, if it's true, you can toggle it with either your right joystick or trigger!");
        }
    }
}