using BepInEx;
using HarmonyLib;
using TheGorillaWatch.Behaviors;
using TheGorillaWatch.Behaviors.MainComponents;

namespace TheGorillaWatch
{
    [BepInPlugin(Constants.GUID, Constants.NAME, Constants.VERS)]
    public class Plugin : BaseUnityPlugin
    {
        private Plugin()
        {
            Harmony.CreateAndPatchAll(typeof(Plugin).Assembly, Constants.GUID);
            ConfigManager.CreateConfig();
            Utilities.Logger.Initialize();

            gameObject.AddComponent<ModInitializer>();
            gameObject.AddComponent<WatchController>();
            gameObject.AddComponent<StartUpSound>();
        }
    }

    internal static class Constants
    {
        public const string GUID = "net.cody.gorillawatch";
        public const string NAME = "GorillaWatch";
        public const string VERS = "3.1.0";
    }
}