using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class NoClip : Page
    {
        public override string modName => "8) NoClip";
        bool resetNoClip = true;
        List<MeshCollider> colliders = new List<MeshCollider>();

        public override void Disable()
        {
            base.Disable();
            foreach (MeshCollider meshCollider2 in colliders)
            {
                meshCollider2.enabled = true;
            }
            resetNoClip = false;
        }

        public override void OnUpdate()
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
                resetNoClip = false;
            }
            else if (!resetNoClip)
            {
                foreach (MeshCollider meshCollider2 in colliders)
                {
                    meshCollider2.enabled = true;
                    colliders.Remove(meshCollider2);
                }
                resetNoClip = true;
            }
        }
    }
}