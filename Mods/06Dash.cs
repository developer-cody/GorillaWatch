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

        public override PageType pageType => PageType.Toggle;

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (ControllerInputPoller.instance.rightControllerPrimaryButton && Time.time - lastDashTime >= dashCooldown)
            {
                Player.Instance.bodyCollider.attachedRigidbody.AddForce(Player.Instance.headCollider.transform.forward * 10, ForceMode.VelocityChange);

                lastDashTime = Time.time;
            }
        }
    }
}
