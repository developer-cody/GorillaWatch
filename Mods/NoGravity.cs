using System;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class NoGravity : Page
    {
        public override string modName => "NoGravity";
        private bool Enabled = false;

        public override void OnUpdate()
        {
            if (Enabled)
            {
                if (Physics.gravity != Vector3.zero)
                {
                    Physics.gravity = Vector3.zero;
                }
            }
            else
            {
                if (Physics.gravity != new Vector3(0, -9.81f, 0))
                {
                    Physics.gravity = new Vector3(0, -9.81f, 0);
                }
            }
        }

        public void ToggleDhisGravThingamagig()
        {
            Enabled = !Enabled;
            Debug.Log("NoGravity Mod: " + (Enabled ? "Enabled" : "Disabled"));
        }
    }
}
