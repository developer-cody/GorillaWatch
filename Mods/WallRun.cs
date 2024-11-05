using GorillaLocomotion;
using System.Reflection;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class WallRun : Page
    {
        public override string modName => "MonkeWallWalk";

        private Vector3 walkPos = Vector3.zero;
        private Vector3 walkNormal = Vector3.zero;
        private bool rightGrab = true;

        public override PageType pageType => PageType.Toggle;

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (GorillaLocomotion.Player.Instance.wasLeftHandTouching || GorillaLocomotion.Player.Instance.wasRightHandTouching)
            {
                FieldInfo fieldInfo = typeof(GorillaLocomotion.Player).GetField("lastHitInfoHand", BindingFlags.NonPublic | BindingFlags.Instance);
                RaycastHit ray = (RaycastHit)fieldInfo.GetValue(GorillaLocomotion.Player.Instance);
                walkPos = ray.point;
                walkNormal = ray.normal;
            }

            if (walkPos != Vector3.zero && rightGrab)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(walkNormal * -9.81f, ForceMode.Acceleration);
                ZeroGravity();
            }
        }

        public override void Enable()
        {
            base.Enable();

        }

        public override void Disable()
        {
            base.Disable();
            Physics.gravity = new Vector3(0f, -9.81f, 0f);
        }

        private void ZeroGravity()
        {
            Physics.gravity = Vector3.zero;
        }
    }
}
