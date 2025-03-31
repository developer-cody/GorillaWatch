using GorillaLocomotion;
using HarmonyLib;
using TheGorillaWatch.Mods;
using UnityEngine;

namespace TheGorillaWatch.Patches
{
    [HarmonyPatch(typeof(GTPlayer), "GetSlidePercentage")]
    public class WallRunPatch
    {
        static Vector3 ogGravity = Physics.gravity;
        
        static void Postfix(RaycastHit raycastHit)
        {
            if (WallRun.MonkeWallWalkEnabled)
            {
                Physics.gravity = raycastHit.normal * -9.81f;
            }
            else 
            {
                Physics.gravity = ogGravity;
            }
        }
    }
}
