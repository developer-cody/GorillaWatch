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
                Player.Instance.GetComponent<Rigidbody>().velocity = Player.Instance.headCollider.transform.forward * Time.deltaTime * 1000f;
            }

            if (ControllerInputPoller.instance.leftGrab)
            {
                Player.Instance.GetComponent<Rigidbody>().velocity = Player.Instance.headCollider.transform.forward * Time.deltaTime * -1000f;
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
