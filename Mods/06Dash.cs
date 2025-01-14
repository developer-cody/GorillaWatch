using GorillaLocomotion;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class DashMonk : Page
    {
        public override string modName => "DashMonk";
        public override List<string> incompatibleModNames => new List<string>() { "VelocityFly", "IronMonke" };
        public override PageType pageType => PageType.Toggle;

        private bool buttonWasPressed = false;
        private float dashForce = 10f;

        public override void OnUpdate()
        {
            base.OnUpdate();

            bool isPressed = ControllerInputPoller.instance.rightControllerPrimaryButton;

            if (isPressed && !buttonWasPressed)
            {
                Rigidbody playerRigidbody = Player.Instance.GetComponent<Rigidbody>();
                Vector3 dashDirection = Player.Instance.headCollider.transform.forward.normalized;

                playerRigidbody.velocity += dashDirection * dashForce * Player.Instance.scale;

                buttonWasPressed = true; 
            }
            else if (!isPressed)
            {
                buttonWasPressed = false;
            }
        }
    }
}
