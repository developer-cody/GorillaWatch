using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace TheGorillaWatch.Patches
{
    public class HarmonyPatches
    {
        private static readonly Harmony instance = new Harmony(Constants.GUID);

        public static bool IsPatched { get; private set; }

        internal static void ApplyHarmonyPatches()
        {
            if (IsPatched) return;

            try
            {
                Debug.Log($"{Constants.Name} is Applying Harmony patches...");
                instance.PatchAll(Assembly.GetExecutingAssembly());
                IsPatched = true;
                Debug.Log($"Harmony patches applied successfully, in {Constants.Name}.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error applying Harmony patches: {ex.Message}");
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            if (!IsPatched) return;

            try
            {
                Debug.Log("Removing Harmony patches...");
                instance.UnpatchSelf();
                IsPatched = false;
                Debug.Log("Harmony patches removed successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error removing Harmony patches: {ex.Message}");
            }
        }
    }
}