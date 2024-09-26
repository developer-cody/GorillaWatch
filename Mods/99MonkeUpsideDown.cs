using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class MonkeFlipped : Page
    {
        public override string modName => "17) MonkeFlipped";

        public override void Enable()
        {
            base.Enable();
            GorillaTagger.Instance.offlineVRRig.transform.eulerAngles = new Vector3(0, 0, 180);
            GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles = new Vector3(GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles.x, GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles.y, 180);
            Physics.gravity *= -1;
        }

        public override void Disable()
        {
            base.Disable();
            GorillaTagger.Instance.offlineVRRig.transform.eulerAngles = new Vector3(0, 0, 0);
            GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles = new Vector3(GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles.x, GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles.y, 0);
            Physics.gravity = new Vector3(0f, -9.807f, 0f);
        }
    }
}
