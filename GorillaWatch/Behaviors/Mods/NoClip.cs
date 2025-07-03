using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class Noclip : ModPage
    {
        public override string modName => "Noclip";
        public override List<string> requiredModNames => new List<string>() { "PlatformGuy" };

        private List<Collider> colliders = new List<Collider>();

        public override void Enable()
        {
            base.Enable();

            Collider[] array = FindObjectsOfType<Collider>();

            foreach (var collider in array)
            {
                if (collider.enabled)
                {
                    colliders.Add(collider);
                    collider.enabled = false;
                }
            }
        }

        public override void Disable()
        {
            base.Disable();

            foreach (var collider in colliders)
            {
                if (collider != null)
                    collider.enabled = true;
            }

            colliders.Clear();
        }

        public override PageType pageType => PageType.Toggle;
    }
}