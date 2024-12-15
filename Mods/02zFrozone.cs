using GorillaLocomotion;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Frozone : Page
    {
        public override string modName => "Frozone";
        public override List<string> incompatibleModNames => new List<string>() { "PlatformGuy" };

        GameObject FrozoneL;
        GameObject FrozoneR;

        float defaultXValue = .02f;
        float defaultYValue = .270f;
        float defaultZValue = .353f;

        public override void Disable()
        {
            base.Disable();
            Destroy(FrozoneR);
            Destroy(FrozoneL);
        }

        public override void Enable()
        {
            base.Enable();
        }

        public override void OnUpdate()
        {
            Vector3 leftOffset = new Vector3(0f, -0.06f, 0f);
            Vector3 rightOffset = new Vector3(0f, -0.06f, 0f);

            if (ControllerInputPoller.instance.leftGrab)
            {
                FrozoneL = GameObject.CreatePrimitive(PrimitiveType.Cube);

                if (Player.Instance.scale != 1f)
                {
                    FrozoneL.transform.localScale = new Vector3(defaultXValue / Player.Instance.scale, defaultYValue / Player.Instance.scale, defaultZValue / Player.Instance.scale);
                }
                else
                {
                    FrozoneL.transform.localScale = new Vector3(defaultXValue, defaultYValue, defaultZValue);
                }

                FrozoneL.transform.position = GorillaTagger.Instance.leftHandTransform.position + leftOffset;
                FrozoneL.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                FrozoneL.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                FrozoneL.AddComponent<GorillaSurfaceOverride>().overrideIndex = 61;
                FrozoneL.GetComponent<Renderer>().material.color = Color.cyan;
                Destroy(FrozoneL, .1f);
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                FrozoneR = GameObject.CreatePrimitive(PrimitiveType.Cube);

                if (Player.Instance.scale != 1f)
                {
                    FrozoneR.transform.localScale = new Vector3(defaultXValue / Player.Instance.scale, defaultYValue / Player.Instance.scale, defaultZValue / Player.Instance.scale);
                }
                else
                {
                    FrozoneR.transform.localScale = new Vector3(defaultXValue, defaultYValue, defaultZValue);
                }

                FrozoneR.transform.position = GorillaTagger.Instance.rightHandTransform.position + rightOffset;
                FrozoneR.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                FrozoneR.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                FrozoneR.AddComponent<GorillaSurfaceOverride>().overrideIndex = 61;
                FrozoneR.GetComponent<Renderer>().material.color = Color.cyan;
                Destroy(FrozoneR, .1f);
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}