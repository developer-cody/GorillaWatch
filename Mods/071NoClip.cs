﻿using System;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Noclip : Page
    {
        public override List<string> requiredModNames => new List<string>() { "PlatformGuy" };
        public override string modName => "Noclip";

        public override void Enable()
        {
            base.Enable();
            ToggleColliders(false);
        }

        public override void Disable()
        {
            base.Disable();
            ToggleColliders(true);
        }

        private void ToggleColliders(bool enable)
        {
            try
            {
                MeshCollider[] array = FindObjectsOfType<MeshCollider>();
                foreach (MeshCollider meshCollider in array)
                {
                    meshCollider.enabled = enable;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Error with Noclip Toggle: {e.Message}");
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}