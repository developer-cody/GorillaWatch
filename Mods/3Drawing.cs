using CjLib;
using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
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
                DrawL.transform.position = Player.Instance.leftControllerTransform.position;
                DrawL.transform.rotation = Player.Instance.leftControllerTransform.rotation;
                DrawL.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                DrawL.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                DrawL.GetComponent<Renderer>().material.color = Color.black;
                GameObject.Destroy(DrawL.GetComponent<Rigidbody>());
                GameObject.Destroy(DrawL.GetComponent<SphereCollider>());
                GameObject.Destroy(DrawL, 10f);
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                DrawR = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                DrawR.transform.position = Player.Instance.rightControllerTransform.position;
                DrawR.transform.rotation = Player.Instance.rightControllerTransform.rotation;
                DrawR.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                DrawR.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                DrawR.GetComponent<Renderer>().material.color = Color.cyan;
                GameObject.Destroy(DrawR.GetComponent<Rigidbody>());
                GameObject.Destroy(DrawR.GetComponent<SphereCollider>());
                GameObject.Destroy(DrawR, 10f);
            }
        }
    }
}
