using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class NoClip : Page
    {
        public override string modName => "NoClip";
        bool resetNoClip = true;

        public override void Disable()
        {
            base.Disable();
            MeshCollider[] array3 = Resources.FindObjectsOfTypeAll<MeshCollider>();
            foreach (MeshCollider meshCollider2 in array3)
            {
                meshCollider2.enabled = true;
            }
            resetNoClip = false;
        }

        public override void OnUpdate()
        {
            if(ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                MeshCollider[] array = Resources.FindObjectsOfTypeAll<MeshCollider>();
                foreach (MeshCollider meshCollider in array)
                {
                    meshCollider.enabled = false;
                }
                resetNoClip = false;
            }
            else if(!resetNoClip)
            {
                MeshCollider[] array3 = Resources.FindObjectsOfTypeAll<MeshCollider>();
                foreach (MeshCollider meshCollider2 in array3)
                {
                    meshCollider2.enabled = true;
                }
                resetNoClip = true;
            }
        }
    }
}
