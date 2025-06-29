using System.Collections.Generic;
using GorillaLocomotion;
using TheGorillaWatch.Behaviors.Page;
using TheGorillaWatch.Configuration;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class Fly : ModPage
    {
        public override string modName => "VelocityFly";
        public override List<string> incompatibleModNames => new List<string> { "IronMonke", "DashMonk" };

        private float vfSpeed = 1500f;

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GTPlayer.Instance.GetComponent<Rigidbody>().velocity =
                    GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime * vfSpeed * GTPlayer.Instance.scale;
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}