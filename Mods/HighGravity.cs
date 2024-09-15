using System;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class HighGravity : Page
    {
        public override string modName => "HighGravity";
        private bool Enabled = false;

        public override void OnUpdate()
        {
            if (Enabled)
            {
                Rigidbody playerRigidbody = GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody;

                playerRigidbody.AddForce(Vector3.down * 7.77f, ForceMode.Acceleration);
            }
        }

        public void ToggleHighGravity()
        {
            Enabled = !Enabled;
            Debug.Log("HighGravity Mod: " + (Enabled ? "Enabled" : "Disabled"));
        }
    }
}
