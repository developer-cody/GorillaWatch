using GorillaLocomotion;
using System.Reflection;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class WallRun : Page
    {
        public override string modName => "MonkeWallWalk";
        public override PageType pageType => PageType.Toggle;

        Vector3 walkPos = Vector3.zero;
        Vector3 walkNormal = Vector3.zero;

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (Player.Instance.wasLeftHandTouching || Player.Instance.wasRightHandTouching)
            {
                FieldInfo fieldInfo = typeof(Player).GetField("lastHitInfoHand", BindingFlags.NonPublic | BindingFlags.Instance);
                RaycastHit ray = (RaycastHit)fieldInfo.GetValue(Player.Instance);
                walkPos = ray.point;
                walkNormal = ray.normal;
            }

            if (walkPos != Vector3.zero)
            {
                Player.Instance.bodyCollider.attachedRigidbody.AddForce(walkNormal * -9.81f, ForceMode.Acceleration);

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

            Physics.gravity = new Vector3(0f, -9.8f, 0f);
            walkPos = Vector3.zero;

            Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
        }

        private void ZeroGravity()
        {
            Physics.gravity = Vector3.zero;
        }
    }
}
