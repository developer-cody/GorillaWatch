using GorillaLocomotion;
using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class IronMonke : ModPage
    {
        public override string modName => "IronMonke";
        public override List<string> incompatibleModNames => new List<string>() { "VelocityFly", "DashMonk" };

        private const float forceMultiplier = 10f;
        private const float vibrationDivider = 25f;

        public override void OnUpdate()
        {

            if (ControllerInputPoller.instance.leftGrab)
            {
                Vector3 leftForce = -GorillaTagger.Instance.leftHandTransform.right * forceMultiplier * GTPlayer.Instance.scale;
                GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(leftForce, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / vibrationDivider * 
                    GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                Vector3 rightForce = GorillaTagger.Instance.rightHandTransform.right * forceMultiplier * GTPlayer.Instance.scale;
                GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(rightForce, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / vibrationDivider * 
                    GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
            }
        }
        
        public override PageType pageType => PageType.Toggle;
    }
}