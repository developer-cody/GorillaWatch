using GorillaLocomotion;
using HarmonyLib;
using TheGorillaWatch.Mods;

namespace TheGorillaWatch.Patches
{
    [HarmonyPatch(typeof(Player), "GetSlidePercentage")]
    public class SlipPatches
    {
        private static void Postfix(ref float slipValue)
        {
            if (SlipMonk.SlipMonkEnabled)
            {
                slipValue = 1f;
            }

            if (NoSlip.NoSlipEnabled)
            {
                slipValue = 0f;
            }
        }
    }
}