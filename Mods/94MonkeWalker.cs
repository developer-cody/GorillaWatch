using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class MonkeWalker : Page
    {
        public override string modName => "15) MonkeWalker";

        private GameObject playerColliderParent;

        public override void Disable()
        {
            base.Disable();
            Destroy(playerColliderParent);
        }

        public override void OnUpdate()
        {
            if (playerColliderParent != null)
            {
                Destroy(playerColliderParent);
            }
            
            playerColliderParent = new GameObject();
            
            foreach (VRRig vrig in GorillaParent.instance.vrrigs)
            {
                if (vrig != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject thisPlayerCollider = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    thisPlayerCollider.transform.position = vrig.transform.position;
                    thisPlayerCollider.GetComponent<Renderer>().enabled = false;
                    thisPlayerCollider.transform.localScale = new Vector3(0.3f, 0.55f, 0.3f);
                    thisPlayerCollider.transform.rotation = vrig.transform.rotation;
                    thisPlayerCollider.transform.SetParent(playerColliderParent.transform, false);
            
                    if (thisPlayerCollider.GetComponent<BoxCollider>() != null)
                    {
                        thisPlayerCollider.GetComponent<BoxCollider>().enabled = true;
                    }
                    else
                    {
                        thisPlayerCollider.AddComponent<BoxCollider>();
                    }
            
                    Rigidbody rb = thisPlayerCollider.AddComponent<Rigidbody>();
                    rb.isKinematic = true;
                    rb.useGravity = false;
            
                    Rigidbody handRigidbody = vrig.gameObject.GetComponent<Rigidbody>();
                    if (handRigidbody != null)
                    {
                        handRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                    }
                }
            }
        }
    }
}
