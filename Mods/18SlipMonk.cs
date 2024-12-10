using System;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class SlipMonk : Page
    {
        public override string modName => "SlipMonk";

        public override void Disable()
        {
            base.Disable();
            MeshCollider[] array = FindObjectsOfType<MeshCollider>();
            foreach (MeshCollider meshCollider in array)
            {
                if (meshCollider.enabled)
                {
                    GorillaSurfaceOverride surfaceOverride = meshCollider.GetComponent<GorillaSurfaceOverride>();
                    if (surfaceOverride == null)
                    {
                        surfaceOverride = meshCollider.AddComponent<GorillaSurfaceOverride>();
                    }

                    if (surfaceOverride.overrideIndex != 0)
                    {
                        surfaceOverride.overrideIndex = 0;
                    }

                    surfaceOverride.enabled = false;
                }
            }
        }

        public override void Enable()
        {
            base.Enable();
            try
            {
                MeshCollider[] array = FindObjectsOfType<MeshCollider>();
                foreach (MeshCollider meshCollider in array)
                {
                    if (meshCollider.enabled)
                    {
                        GorillaSurfaceOverride surfaceOverride = meshCollider.GetComponent<GorillaSurfaceOverride>();
                        if (surfaceOverride == null)
                        {
                            surfaceOverride = meshCollider.AddComponent<GorillaSurfaceOverride>();
                        }

                        if (surfaceOverride.overrideIndex != 61)
                        {
                            surfaceOverride.overrideIndex = 61;
                        }

                        surfaceOverride.enabled = true;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Error with SlipMonk: {e.Message}");
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}
