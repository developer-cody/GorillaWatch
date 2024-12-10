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

        private float lastDashTime = 0f;
        private readonly float dashCooldown = 0.8f;
        private readonly Vector3 dashDirection;

        public DashMonk()
        {
            dashDirection = Player.Instance.headCollider.transform.forward;
        }

        public override PageType pageType => PageType.Toggle;

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton && Time.time - lastDashTime >= dashCooldown)
            {
                Player.Instance.bodyCollider.attachedRigidbody.AddForce(dashDirection * 10f, ForceMode.VelocityChange);

                lastDashTime = Time.time;
            }
        }
    }
}
