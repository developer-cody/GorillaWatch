using GorillaLocomotion;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class IronMonke : Page
    {
        public override string modName => "IronMonke";
        public override List<string> incompatibleModNames => new List<string> { "VelocityFly", "DashMonk" };

        private const float forceMultiplier = 10f;
        private const float vibrationStrengthDivisor = 50f;

        public override void OnUpdate()
        {
            HandleHandForce(ControllerInputPoller.instance.leftGrab, GorillaTagger.Instance.leftHandTransform, true);
            HandleHandForce(ControllerInputPoller.instance.rightGrab, GorillaTagger.Instance.rightHandTransform, false);
        }

        private void HandleHandForce(bool isGrabbed, Transform handTransform, bool isLeftHand)
        {
            if (isGrabbed)
            {
                ApplyForce(handTransform, isLeftHand);
                TriggerVibration(isLeftHand);
            }
        }

        private void ApplyForce(Transform handTransform, bool isLeftHand)
        {
            Vector3 forceDirection = isLeftHand ? -handTransform.right : handTransform.right;
            Player.Instance.bodyCollider.attachedRigidbody.AddForce(forceMultiplier * Player.Instance.scale * forceDirection, ForceMode.Acceleration);
        }

        private void TriggerVibration(bool isLeftHand)
        {
            float vibrationStrength = GorillaTagger.Instance.tapHapticStrength / vibrationStrengthDivisor * Player.Instance.bodyCollider.attachedRigidbody.velocity.magnitude;
            GorillaTagger.Instance.StartVibration(isLeftHand, vibrationStrength, GorillaTagger.Instance.tapHapticDuration);
        }

        public override PageType pageType => PageType.Toggle;
    }
}