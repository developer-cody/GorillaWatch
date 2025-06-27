using GorillaLocomotion;
using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class Fly : ModPage
    {
        public override string modName => "VelocityFly";
        public override List<string> incompatibleModNames => new List<string> { "IronMonke", "DashMonk" };

        private float flyForce = 1000f;

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GTPlayer.Instance.GetComponent<Rigidbody>().velocity =
                    GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime * flyForce * GTPlayer.Instance.scale;
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}