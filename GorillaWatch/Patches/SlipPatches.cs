using GorillaLocomotion;
using HarmonyLib;
using TheGorillaWatch.Behaviors.Mods;

namespace TheGorillaWatch.Patches
{
    [HarmonyPatch(typeof(GTPlayer), "GetSlidePercentage")]
    public class SlipPatches
    {
        static void Postfix(ref float __result)
        {
            if (SlipMonk.SlipMonkEnabled) __result = 1f; 
            if (NoSlip.NoSlipEnabled) __result = 0f;
        }
    }
}