using GorillaLocomotion;
using HarmonyLib;
using TheGorillaWatch.Mods;
using UnityEngine;

namespace TheGorillaWatch.Patches
{
    [HarmonyPatch(typeof(GTPlayer), "GetSlidePercentage")]
    public class WallRunPatch
    {
        static void Postfix(RaycastHit raycastHit)
        {
            if (WallRun.MonkeWallWalkEnabled)
            {
                Physics.gravity = raycastHit.normal * -9.81f;
            }
        }
    }
}