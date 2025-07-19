using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class PlatformGuy : ModPage
    {
        public override string modName => "PlatformGuy";
        public override List<string> incompatibleModNames => new List<string>() { "FrozoneGuy" };

        GameObject leftplat;
        GameObject rightplat;

        public override void Disable()
        {
            base.Disable();
            Destroy(leftplat);
            Destroy(rightplat);
        }

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (leftplat == null)
                {
                    leftplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    leftplat.transform.localScale = new Vector3(.02f, .27f, .353f);
                    leftplat.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, -0.06f, 0f);
                    leftplat.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                    leftplat.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;
                    leftplat.GetComponent<Renderer>().material.color = GorillaTagger.Instance.offlineVRRig.mainSkin.material.color;
                }
            }
            else
            {
                if (leftplat != null)
                {
                    Destroy(leftplat);
                    leftplat = null;
                }
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                if (rightplat == null)
                {
                    rightplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    rightplat.transform.localScale = new Vector3(.02f, .27f, .353f);
                    rightplat.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, -0.06f, 0f);
                    rightplat.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                    rightplat.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;
                    rightplat.GetComponent<Renderer>().material.color = GorillaTagger.Instance.offlineVRRig.mainSkin.material.color;
                }
            }
            else
            {
                if (rightplat != null)
                {
                    Destroy(rightplat);
                    rightplat = null;
                }
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}