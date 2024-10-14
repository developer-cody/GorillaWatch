using System;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class SlipMonk : Page
    {
        public override string modName => "NoClip";
        List<MeshCollider> colliders = new List<MeshCollider>();

        public override void Disable()
        {
            foreach (MeshCollider meshcollider2 in colliders)
            {
                meshcollider2.AddComponent<GorillaSurfaceOverride>().enabled = false;
            }
        }

        public override void OnUpdate()
        {
            try
            {
                MeshCollider[] array = FindObjectsOfType<MeshCollider>();
                foreach (MeshCollider meshCollider in array)
                {
                    if (meshCollider.enabled)
                    {
                        meshCollider.AddComponent<GorillaSurfaceOverride>().enabled = true;
                        meshCollider.AddComponent<GorillaSurfaceOverride>().overrideIndex = 51;
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
