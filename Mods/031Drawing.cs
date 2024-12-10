using GorillaLocomotion;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Drawing : Page
    {
        public override string modName => "DrawingGuy";

        private GameObject drawLeft;
        private GameObject drawRight;
        private const float sphereScale = 0.2f;

        public override void Disable()
        {
            base.Disable();
            DestroyDrawObjects();
        }

        public override void Enable()
        {
            base.Enable();
        }

        public override void OnUpdate()
        {
            HandleDrawing(ControllerInputPoller.instance.leftGrab, ref drawLeft, Player.Instance.leftControllerTransform, Color.black);
            HandleDrawing(ControllerInputPoller.instance.rightGrab, ref drawRight, Player.Instance.rightControllerTransform, Color.cyan);
        }

        private void HandleDrawing(bool isGrabbed, ref GameObject drawObject, Transform controllerTransform, Color color)
        {
            if (isGrabbed)
            {
                if (drawObject == null)
                {
                    CreateDrawObject(ref drawObject, controllerTransform, color);
                }
                UpdateDrawObjectPosition(drawObject, controllerTransform);
            }
            else if (drawObject != null)
            {
                Destroy(drawObject);
                drawObject = null;
            }
        }

        private void CreateDrawObject(ref GameObject drawObject, Transform controllerTransform, Color color)
        {
            drawObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            drawObject.transform.localScale = Vector3.one * sphereScale;
            drawObject.transform.position = controllerTransform.position;
            drawObject.transform.rotation = controllerTransform.rotation;

            var renderer = drawObject.GetComponent<Renderer>();
            renderer.material.shader = Shader.Find("GorillaTag/UberShader");
            renderer.material.color = color;

            Destroy(drawObject.GetComponent<Rigidbody>());
            Destroy(drawObject.GetComponent<SphereCollider>());
            Destroy(drawObject, 5f);
        }

        private void UpdateDrawObjectPosition(GameObject drawObject, Transform controllerTransform)
        {
            drawObject.transform.position = controllerTransform.position;
            drawObject.transform.rotation = controllerTransform.rotation;
        }

        private void DestroyDrawObjects()
        {
            if (drawLeft != null)
            {
                Destroy(drawLeft);
                drawLeft = null;
            }
            if (drawRight != null)
            {
                Destroy(drawRight);
                drawRight = null;
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}