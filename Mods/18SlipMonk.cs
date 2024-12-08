using System;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class SlipMonk : Page
    {
        public override string modName => "SlipMonk";
        List<MeshCollider> colliders = new List<MeshCollider>();

        public override void Disable()
        {
            base.Disable();
            MeshCollider[] array = FindObjectsOfType<MeshCollider>();
            foreach (MeshCollider meshCollider in array)
            {
                if (meshCollider.enabled)
                {
                    meshCollider.AddComponent<GorillaSurfaceOverride>();
                    meshCollider.GetComponent<GorillaSurfaceOverride>().enabled = false;
                    meshCollider.GetComponent<GorillaSurfaceOverride>().overrideIndex = 0;
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
                        meshCollider.AddComponent<GorillaSurfaceOverride>();
                        meshCollider.GetComponent<GorillaSurfaceOverride>().enabled = true;
                        meshCollider.GetComponent<GorillaSurfaceOverride>().overrideIndex = 61;
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