using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class Noclip : ModPage
    {
        public override List<string> requiredModNames => new List<string>() { "PlatformGuy" };
        public override string modName => "Noclip";

        public override void Enable()
        {
            base.Enable();
            MeshCollider[] array = FindObjectsOfType<MeshCollider>();
            foreach (MeshCollider meshCollider in array)
            {
                meshCollider.enabled = false;
            }
        }

        public override void Disable()
        {
            base.Disable();
            MeshCollider[] array = FindObjectsOfType<MeshCollider>();
            foreach (MeshCollider meshCollider in array)
            {
                meshCollider.enabled = true;
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}