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

            gameObject.AddComponent<ModInitializer>();
            gameObject.AddComponent<WatchController>();
            gameObject.AddComponent<StartUpSound>();
        }
    }
}