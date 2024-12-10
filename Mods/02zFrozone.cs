using System.Collections;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Frozone : Page
    {
        public override string modName => "Frozone";
        public override List<string> incompatibleModNames => new List<string>() { "PlatformGuy" };

        private GameObject FrozoneL;
        private GameObject FrozoneR;
        private Coroutine leftFrozoneCoroutine;
        private Coroutine rightFrozoneCoroutine;

        private const float cubeLifeTime = 0.1f;
        private readonly Vector3 offset = new Vector3(0f, -0.06f, 0f);
        private readonly Vector3 cubeScale = new Vector3(0.02f, 0.270f, 0.353f);

        public override void Disable()
        {
            base.Disable();
            DestroyFrozoneObjects();
        }

        public override void Enable()
        {
            base.Enable();
        }

        public override void OnUpdate()
        {
            if (ControllerInputPoller.instance.leftGrab && leftFrozoneCoroutine == null)
            {
                leftFrozoneCoroutine = StartCoroutine(CreateFrozoneCube(GorillaTagger.Instance.leftHandTransform, true));
            }

            if (ControllerInputPoller.instance.rightGrab && rightFrozoneCoroutine == null)
            {
                rightFrozoneCoroutine = StartCoroutine(CreateFrozoneCube(GorillaTagger.Instance.rightHandTransform, false));
            }

            if (!ControllerInputPoller.instance.leftGrab && leftFrozoneCoroutine != null)
            {
                StopCoroutine(leftFrozoneCoroutine);
                Destroy(FrozoneL);
                leftFrozoneCoroutine = null;
            }

            if (!ControllerInputPoller.instance.rightGrab && rightFrozoneCoroutine != null)
            {
                StopCoroutine(rightFrozoneCoroutine);
                Destroy(FrozoneR);
                rightFrozoneCoroutine = null;
            }
        }

        private IEnumerator CreateFrozoneCube(Transform handTransform, bool isLeft)
        {
            GameObject frozone = GameObject.CreatePrimitive(PrimitiveType.Cube);
            frozone.transform.localScale = cubeScale;
            frozone.transform.position = handTransform.position + offset;
            frozone.transform.rotation = handTransform.rotation;

            frozone.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            frozone.AddComponent<GorillaSurfaceOverride>().overrideIndex = 61;
            frozone.GetComponent<Renderer>().material.color = Color.cyan;

            if (isLeft)
            {
                FrozoneL = frozone;
            }
            else
            {
                FrozoneR = frozone;
            }

            yield return new WaitForSeconds(cubeLifeTime);

            Destroy(frozone);
        }

        private void DestroyFrozoneObjects()
        {
            if (FrozoneL != null)
                Destroy(FrozoneL);

            if (FrozoneR != null)
                Destroy(FrozoneR);
        }

        public override PageType pageType => PageType.Toggle;
    }
}