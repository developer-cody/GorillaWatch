using System.Collections.Generic;
using GorillaLocomotion;
using TheGorillaWatch.Behaviors.Page;
using TheGorillaWatch.Configuration;
using UnityEngine;
using Valve.VR;

namespace TheGorillaWatch.Behaviors.Mods
{
    class Fly : ModPage
    {
        public override string modName => "VelocityFly";
        public override List<string> incompatibleModNames => new List<string> { "IronMonke", "DashMonk" };

        private float vfSpeed = 1500f;
        private float jfSpeed = 10f;

        public override void OnUpdate()
        {
            if (ConfigManager.flyType.Value.Equals(1))
            {
                if (ControllerInputPoller.instance.rightControllerPrimaryButton)
                {
                    GTPlayer.Instance.GetComponent<Rigidbody>().velocity =
                        GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime * vfSpeed * GTPlayer.Instance.scale;
                }
            }
            else
            {
                Rigidbody component = GTPlayer.Instance.GetComponent<Rigidbody>();
                SphereCollider headCollider = GTPlayer.Instance.headCollider;
                Vector2 axis = SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.GetAxis(SteamVR_Input_Sources.LeftHand);
                Vector3 forward = headCollider.transform.forward;
                Vector3 right = headCollider.transform.right;
                Vector3 vector = axis.x * right + axis.y * forward;
                Vector3 vector2 = new Vector3(0f, axis.y, 0f);
                Vector3 vector3 = (vector + vector2) * jfSpeed;

                component.AddForce(vector3 - component.velocity, ForceMode.VelocityChange);
                component.AddForce(-Physics.gravity, ForceMode.Acceleration);
                Noclip.ToggleColliders(false);
            }
        }

        public override void Disable() => Noclip.ToggleColliders(false);
        public override PageType pageType => PageType.Toggle;
    }
}