using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class IronMonke : Page
    {
        public override string modName => "6) IronMonke";

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.leftControllerIndexFloat > .5f)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(10 * GorillaTagger.Instance.offlineVRRig.transform.Find("rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L").right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 50f * GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
            }
            if (ControllerInputPoller.instance.rightControllerIndexFloat > .5f)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(10 * -GorillaTagger.Instance.offlineVRRig.transform.Find("rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R").right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 50f * GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
            }
        }
    }
}
