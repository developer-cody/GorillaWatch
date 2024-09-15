using System;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class LowGravity : Page
    {
        public override string modName => "LowGravity";
        private bool Enabled = false;

        public override void OnUpdate()
        {
            if (Enabled)
            {
                Rigidbody playerRigidbody = GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody;

                playerRigidbody.AddForce(Vector3.up * 6.66f, ForceMode.Acceleration);
            }
        }

        public void ToggleLowGravity()
        {
            Enabled = !Enabled;
            Debug.Log("LowGravity Mod: " + (Enabled ? "Enabled" : "Disabled"));
        }
    }
}
