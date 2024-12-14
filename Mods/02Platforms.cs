﻿using System.Collections.Generic;
using TheGorillaWatch.Configuration;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Platforms : Page
    {
        public override string modName => "PlatformGuy";
        public override List<string> incompatibleModNames => new List<string>() { "FrozoneGuy" };

        GameObject leftplat;
        GameObject rightplat;
        Vector3 leftOffset = new Vector3(0f, -0.06f, 0f);
        Vector3 rightOffset = new Vector3(0f, -0.06f, 0f);

        private float colorChangeSpeed = 1f;
        private float timeElapsed = 0f;

        public override void Disable()
        {
            base.Disable();
            Destroy(leftplat);
            Destroy(rightplat);
        }

        public override void Enable()
        {
            base.Enable();
        }

        public override void OnUpdate()
        {
            Color playerColor = GorillaTagger.Instance.offlineVRRig.mainSkin.material.color;
            Color newColor = Color.HSVToRGB(Mathf.PingPong(timeElapsed, 2f), 1f, 1f);

            timeElapsed += Time.deltaTime * colorChangeSpeed;


            if (ControllerInputPoller.instance.leftGrab)
            {
                if (leftplat == null)
                {
                    leftplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    leftplat.transform.localScale = new Vector3(0.02f, 0.270f, 0.353f);
                    leftplat.transform.position = GorillaTagger.Instance.leftHandTransform.position + leftOffset;
                    leftplat.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                    leftplat.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                }

                if (ConfigManager.platformConfig.Value)
                {
                    leftplat.GetComponent<Renderer>().material.color = newColor;
                }
                else
                {
                    leftplat.GetComponent<Renderer>().material.color = playerColor;
                }
            }
            else
            {
                if (leftplat != null)
                {
                    GameObject.Destroy(leftplat);
                    leftplat = null;
                }
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                if (rightplat == null)
                {
                    rightplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    rightplat.transform.localScale = new Vector3(0.02f, 0.270f, 0.353f);
                    rightplat.transform.position = GorillaTagger.Instance.rightHandTransform.position + rightOffset;
                    rightplat.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                    rightplat.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                }

                if (ConfigManager.platformConfig.Value)
                {
                    rightplat.GetComponent<Renderer>().material.color = newColor;
                }
                else
                {
                    rightplat.GetComponent<Renderer>().material.color = playerColor;
                }
            }
            else
            {
                if (rightplat != null)
                {
                    GameObject.Destroy(rightplat);
                    rightplat = null;
                }
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}