using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class LowGravity : Page
    {
        public override string modName => "LowGravity";

        private bool isEnabled = false;

        public override void OnUpdate()
        {
            if (isEnabled)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
            }
        }

        public void ToggleLowGravity()
        {
            isEnabled = !isEnabled;
        }
    }
}
