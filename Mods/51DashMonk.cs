using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class DashMonk : Page
    {
        public override string modName => "5) DashMonk";

        bool holding;

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !holding)
            {
                holding = true;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(GorillaLocomotion.Player.Instance.headCollider.transform.forward * 10, UnityEngine.ForceMode.VelocityChange);
            }
            else if (!ControllerInputPoller.instance.rightControllerPrimaryButton && holding)
            {
                holding = false;
            }
        }
    }
}
