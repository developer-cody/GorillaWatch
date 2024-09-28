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
        List<MeshCollider> colliders = new List<MeshCollider>();

        public override void Disable()
        {
            base.Disable();
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.GetComponent<MeshCollider>() != null && obj.GetComponent<GorillaLocomotion.Swimming.WaterVolume>() == null)
                {
                    obj.GetComponent<MeshCollider>().enabled = true;
                }
            }
            resetNoClip = false;
        }

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                foreach (GameObject obj in FindObjectsOfType<GameObject>())
                {
                    if (obj.GetComponent<MeshCollider>() != null && obj.GetComponent<GorillaLocomotion.Swimming.WaterVolume>() == null && ControllerInputPoller.instance.rightControllerIndexFloat > 0.3f)
                    {
                        obj.GetComponent<MeshCollider>().enabled = false;
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