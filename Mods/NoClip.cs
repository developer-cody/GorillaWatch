using System;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Noclip : Page
    {
        public override string modName => "Noclip (W?)";
        List<MeshCollider> colliders = new List<MeshCollider>();

        public override void Disable()
        {
            base.Disable();
            try
            {
                MeshCollider[] array = FindObjectsOfType<MeshCollider>();
                foreach (MeshCollider meshCollider in array)
                {
                    meshCollider.enabled = true;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Error with Noclip Disable: {e.Message}");
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
                    meshCollider.enabled = false;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Error with Noclip Enable: {e.Message}");
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}
