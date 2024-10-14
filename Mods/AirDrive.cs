using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class AirDrive : Page
    {
        public override string modName => "AirDrive";

        public override void OnUpdate()
        {
            Physics.gravity = Vector3.zero;

            if (ControllerInputPoller.instance.rightGrab)
            {
                Vector3 forceDirection = new Vector3(0, 10, 0);
                GorillaLocomotion.Player.Instance.headCollider.GetComponent<Rigidbody>().AddForce(forceDirection, ForceMode.Acceleration);
            }

            if (ControllerInputPoller.instance.leftGrab)
            {
                Vector3 forceDirection = new Vector3(0, -10, 0);
                GorillaLocomotion.Player.Instance.headCollider.GetComponent<Rigidbody>().AddForce(forceDirection, ForceMode.Acceleration);
            }
        }

        public override void Disable()
        {
            base.Disable();
            Physics.gravity = new Vector3(0f, -9.807f, 0f);
        }
        public override PageType pageType => PageType.Toggle;
    }
}
