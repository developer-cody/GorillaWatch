using System;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class NoClip : Page
    {
        public override string modName => "NoClip";
        private bool resetNoClip = true;
        private List<MeshCollider> colliders = new List<MeshCollider>();

        public override void Disable()
        {
            base.Disable();
            EnableAllColliders();
            resetNoClip = false;
        }

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                DisableColliders();
                resetNoClip = false;
            }
            else if (!resetNoClip)
            {
                EnableAllColliders();
                resetNoClip = true;
            }
        }

        private void DisableColliders()
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
        }

        private void EnableAllColliders()
        {
            foreach (MeshCollider meshCollider in colliders)
            {
                meshCollider.enabled = true;
            }
            colliders.Clear();
        }
    }
}
