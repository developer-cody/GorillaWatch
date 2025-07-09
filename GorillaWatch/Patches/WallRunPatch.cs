using GorillaLocomotion;
using HarmonyLib;
using TheGorillaWatch.Behaviors.Mods;
using TheGorillaWatch.Utilities;
using UnityEngine;

namespace TheGorillaWatch.Patches
{
    [HarmonyPatch(typeof(GTPlayer), "GetSlidePercentage")]
    public class WallRunPatch
    {
        public static Vector3 ogGravity = new Vector3(0, GravityUtils.ogGrav, 0);
        
        static void Postfix(RaycastHit raycastHit)
        {
            if (MonkeWallWalk.MonkeWallWalkEnabled) Physics.gravity = raycastHit.normal * -9.81f; 
            else if (!GravityUtils.isOn()) Physics.gravity = ogGravity; 
        }
    }
}
