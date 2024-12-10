using GorillaLocomotion;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Fly : Page
    {
        public override string modName => "VelocityFly";
        public override List<string> incompatibleModNames => new List<string> { "IronMonke", "DashMonk" };

        private const float flyForce = 1000f;

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                ApplyFlyForce();
            }
        }

        private void ApplyFlyForce()
        {
            Rigidbody playerRigidbody = Player.Instance.GetComponent<Rigidbody>();
            Transform headTransform = Player.Instance.headCollider.transform;

            playerRigidbody.velocity = headTransform.forward * Time.deltaTime * flyForce;
        }

        public override PageType pageType => PageType.Toggle;
    }
}