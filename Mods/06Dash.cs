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

        private bool wasPressed;
        private float flyForce = 1000f;

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !wasPressed)
            {
                Player.Instance.GetComponent<Rigidbody>().velocity =
                    Player.Instance.headCollider.transform.forward * Time.deltaTime * flyForce * Player.Instance.scale;
                wasPressed = true;
            }
            else
            {
                wasPressed = false;
            }
        }
    }
}