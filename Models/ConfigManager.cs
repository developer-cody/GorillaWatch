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
        public static ConfigEntry<float> BigMonkersSize;
        public static ConfigEntry<float> SmallMonkersSize;

        static ConfigFile config;

        public static void CreateConfig()
        {
            config = new ConfigFile(Path.Combine(Paths.ConfigPath, "GorillaWatch.cfg"), true);

            toggleModButton = config.Bind("Settings", "Use Left Trigger for Mod Toggle", false,
                "Choose whether to use your left joystick down or your left trigger to toggle the mod. " +
                "If false, you will use your right joystick click to toggle. If true, you will use your left trigger.");

            toggleWatchButton = config.Bind("Settings", "Use Right Trigger for Watch Toggle", false,
                "Choose whether to use your right joystick down or your right trigger to toggle the watch. " +
                "If false, you will use your right joystick click to toggle. If true, you will use your right trigger.");

            toggleableWatch = config.Bind("Settings", "Do You Want Your Watch to be Toggleable?", true,
                "Choose whether you want your watch to be toggleable with either your right joystick or trigger. " +
                "If false, you cannot toggle the watch. If true, you can toggle it with either your right joystick or trigger!");

            BigMonkersSize = config.Bind("Settings", "How Big Do You Want BigMonkers to Be?", 2f,
                "Choose how big you want the scale to be when BigMonkers is enabled. ");
            if (BigMonkersSize.Value <= 1f)
            {
                BigMonkersSize.Value = 2f;
            }

            SmallMonkersSize = config.Bind("Settings", "How Small Do You Want SmallMonkers to Be?", 0.1f,
                "Choose how small you want the scale to be when SmallMonkers is enabled. ");
            if (SmallMonkersSize.Value >= 1f)
            {
                SmallMonkersSize.Value = .1f;
            }
            else if (SmallMonkersSize.Value < .1f)
            {
                SmallMonkersSize.Value = .1f;
            }
        }
    }
}