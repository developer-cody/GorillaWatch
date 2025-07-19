using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    public class GrapplingGuy : ModPage
    {
        public override string modName => "GrapplingGuy";

        private GameObject leftLineObj, rightLineObj;
        private LineRenderer leftLine, rightLine;
        private SpringJoint leftJoint, rightJoint;
        private bool leftActive, rightActive;
        private Vector3 leftPoint, rightPoint;

        public override void Init()
        {
            leftLineObj = new GameObject("LeftGrappleLine");
            leftLineObj.transform.SetParent(transform, false);
            leftLine = leftLineObj.AddComponent<LineRenderer>();
            SetupLine(leftLine);

            rightLineObj = new GameObject("RightGrappleLine");
            rightLineObj.transform.SetParent(transform, false);
            rightLine = rightLineObj.AddComponent<LineRenderer>();
            SetupLine(rightLine);
        }

        public override void Disable()
        {
            base.Disable();
            if (leftJoint) Destroy(leftJoint);
            if (rightJoint) Destroy(rightJoint);
            leftLine.enabled = false;
            rightLine.enabled = false;
            leftActive = false;
            rightActive = false;
        }

        private void SetupLine(LineRenderer line)
        {
            line.startWidth = 0.01f;
            line.endWidth = 0.01f;
            line.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            line.material.color = new Color(0.9f, 0.9f, 0.9f, 0.8f);
            line.material.SetFloat("_Metallic", 0.8f);
            line.material.SetFloat("_Smoothness", 0.9f);
            line.receiveShadows = true;
            line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            line.positionCount = 2;
            line.enabled = false;
        }

        private void HandleGrapple(bool right, bool grip)
        {
            if (!modEnabled) return;

            var tagger = GorillaTagger.Instance;
            if (tagger == null) return;

            var hand = right ? tagger.rightHandTransform : tagger.leftHandTransform;
            if (hand == null) return;

            var joint = right ? rightJoint : leftJoint;
            var line = right ? rightLine : leftLine;
            var active = right ? rightActive : leftActive;
            var point = right ? rightPoint : leftPoint;

            if (grip)
            {
                if (!active && Physics.Raycast(hand.position, hand.forward, out RaycastHit hit, 100f))
                {
                    point = hit.point;
                    joint = tagger.gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = point;
                    joint.maxDistance = Vector3.Distance(hand.position, point) * 0.9f;
                    joint.minDistance = 0.1f;
                    joint.spring = 100f;
                    joint.damper = 10f;
                    joint.massScale = 2f;
                    line.enabled = true;
                    if (right)
                    {
                        rightPoint = point;
                        rightJoint = joint;
                        rightActive = true;
                    }
                    else
                    {
                        leftPoint = point;
                        leftJoint = joint;
                        leftActive = true;
                    }
                }
            }
            else
            {
                if (joint) Destroy(joint);
                line.enabled = false;
                if (right)
                {
                    rightJoint = null;
                    rightActive = false;
                }
                else
                {
                    leftJoint = null;
                    leftActive = false;
                }
            }
        }

        public override void OnUpdate()
        {
            var poller = ControllerInputPoller.instance;
            if (poller == null) return;

            HandleGrapple(false, poller.leftGrab);
            HandleGrapple(true, poller.rightGrab);

            var tagger = GorillaTagger.Instance;
            if (tagger == null) return;

            if (leftActive && tagger.leftHandTransform)
            {
                leftLine.SetPosition(0, tagger.leftHandTransform.position);
                leftLine.SetPosition(1, leftPoint);
            }

            if (rightActive && tagger.rightHandTransform)
            {
                rightLine.SetPosition(0, tagger.rightHandTransform.position);
                rightLine.SetPosition(1, rightPoint);
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}