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

        private float flyForce = 1000f;

        public override void OnUpdate()
        {
            var scale = Player.Instance.scale;

            if (scale < 1f)
            {
                flyForce = 250f;
            }
            else if (scale > 1f)
            {
                flyForce = 2000f;
            }
            else
            {
                flyForce = 1000f;
            }

            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                Player.Instance.GetComponent<Rigidbody>().velocity = Player.Instance.headCollider.transform.forward * Time.deltaTime * flyForce;
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}