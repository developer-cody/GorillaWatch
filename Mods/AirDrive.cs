using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class DriveMonke : Page
    {
        public override string modName => "DriveMonke";

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Player.Instance.GetComponent<Rigidbody>().velocity = Player.Instance.headCollider.transform.forward * Time.deltaTime * 1400f;
            }

            if (ControllerInputPoller.instance.leftGrab)
            {
                Player.Instance.GetComponent<Rigidbody>().velocity = Player.Instance.headCollider.transform.forward * Time.deltaTime * -1400f;
            }
        }
    }
}
