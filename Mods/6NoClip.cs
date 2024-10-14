using System;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class NoClip : Page
    {
        public override string modName => "NoClip";
        bool resetNoclip = true;
        List<MeshCollider> colliders = new List<MeshCollider>();

        public override void Disable()
        {
            foreach (MeshCollider meshcollider2 in colliders)
            {
                meshcollider2.enabled = false;
            }
            resetNoclip = false;
        }

        public override void OnUpdate()
        {
            try
            {
                if (ControllerInputPoller.instance.rightControllerPrimaryButton)
                {
                    MeshCollider[] array = FindObjectsOfType<MeshCollider>();
                    foreach (MeshCollider meshCollider in array)
                    {
                        if (meshCollider.enabled)
                        {
                            meshCollider.enabled = false;
                            colliders.Add(meshCollider);
                        }
                    }
                    resetNoclip = false;
                }
                else if (!resetNoclip)
                {
                    foreach (MeshCollider meshCollider2 in colliders)
                    {
                        meshCollider2.enabled = true;
                        colliders.Remove(meshCollider2);
                    }
                    resetNoclip = true;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Error with noclip: {e.Message}");
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}
