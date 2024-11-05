using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class DashMonk : Page
    {
        public override string modName => "DashMonk";
        public override List<string> incompatibleModNames => new List<string>() { "VelocityFly", "IronMonke" };

        bool holding;

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !holding)
            {
                holding = true;
                Player.Instance.bodyCollider.attachedRigidbody.AddForce(Player.Instance.headCollider.transform.forward * 10, ForceMode.VelocityChange);
            }
            else if (!ControllerInputPoller.instance.rightControllerPrimaryButton && holding)
            {
                holding = false;
            }
        }

        public override PageType pageType => PageType.Toggle;

    }
}
