using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    public class DashMonke : Page
    {
        public override string modName => "DashMonkey";
        float timer = 0;
        float maxTime = 2;

        public override void OnUpdate()
        {
            if (timer != maxTime)
            {
                timer += Time.deltaTime;
            }
            if (ControllerInputPoller.instance.leftControllerPrimaryButton && timer == maxTime)
            {
                timer = 0;
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.transform.forward * 2f;
            }
        }
    }
}
