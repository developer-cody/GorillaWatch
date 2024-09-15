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
        public override List<string> requiredModNames => new List<string>() { "Platforms" };

        public override void Disable()
        {
            base.Disable();
            foreach (MeshCollider meshCollider2 in FindObjectsOfType<MeshCollider>())
            {
                meshCollider2.enabled = true;
            }
        }



        public override void OnUpdate()
        {
            foreach (MeshCollider meshCollider in FindObjectsOfType<MeshCollider>())
            {
                meshCollider.enabled = false;
            }
        }
    }
}
