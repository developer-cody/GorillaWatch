using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Fly : Page
    {
        public override string modName => "VelocityFly";

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 1400f;
            }
        }
    }
}
