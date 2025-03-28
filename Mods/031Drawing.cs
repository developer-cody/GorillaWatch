﻿using GorillaLocomotion;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Drawing : Page
    {
        public override string modName => "DrawingGuy";

        GameObject DrawL;
        GameObject DrawR;

        public override void Disable()
        {
            base.Disable();
            Destroy(DrawL);
            Destroy(DrawR);
        }

        public override void Enable()
        {
            base.Enable();
        }

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                DrawL = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                DrawL.transform.position = GTPlayer.Instance.leftControllerTransform.position;
                DrawL.transform.rotation = GTPlayer.Instance.leftControllerTransform.rotation;
                DrawL.transform.localScale = new Vector3(.2f, .2f, .2f);
                DrawL.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                DrawL.GetComponent<Renderer>().material.color = Color.black;
                Destroy(DrawL.GetComponent<Rigidbody>());
                Destroy(DrawL.GetComponent<SphereCollider>());
                Destroy(DrawL, 5f);
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                DrawR = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                DrawR.transform.position = GTPlayer.Instance.rightControllerTransform.position;
                DrawR.transform.rotation = GTPlayer.Instance.rightControllerTransform.rotation;
                DrawR.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                DrawR.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                DrawR.GetComponent<Renderer>().material.color = Color.black;
                Destroy(DrawR.GetComponent<Rigidbody>());
                Destroy(DrawR.GetComponent<SphereCollider>());
                Destroy(DrawR, 5f);
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}