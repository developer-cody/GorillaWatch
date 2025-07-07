using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

public class GrapplingGuy : ModPage
{
    public override string modName => "GrapplingGuy";

    private GameObject leftLineObject;
    private GameObject rightLineObject;
    private LineRenderer leftGrappleLine;
    private LineRenderer rightGrappleLine;
    private SpringJoint leftGrappleJoint;
    private SpringJoint rightGrappleJoint;
    private bool isLeftGrappling;
    private bool isRightGrappling;
    private Vector3 leftGrapplePoint;
    private Vector3 rightGrapplePoint;

    public override void Init()
    {
        leftLineObject = new GameObject("LeftGrappleLine");
        leftLineObject.transform.SetParent(transform, false);
        leftGrappleLine = leftLineObject.AddComponent<LineRenderer>();
        leftGrappleLine.startWidth = 0.01f;
        leftGrappleLine.endWidth = 0.01f;
        leftGrappleLine.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        leftGrappleLine.material.color = new Color(0.9f, 0.9f, 0.9f, 0.8f);
        leftGrappleLine.material.SetFloat("_Metallic", 0.8f);
        leftGrappleLine.material.SetFloat("_Smoothness", 0.9f);
        leftGrappleLine.receiveShadows = true;
        leftGrappleLine.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        leftGrappleLine.positionCount = 2;
        leftGrappleLine.enabled = false;

        rightLineObject = new GameObject("RightGrappleLine");
        rightLineObject.transform.SetParent(transform, false);
        rightGrappleLine = rightLineObject.AddComponent<LineRenderer>();
        rightGrappleLine.startWidth = 0.01f;
        rightGrappleLine.endWidth = 0.01f;
        rightGrappleLine.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        rightGrappleLine.material.color = new Color(0.9f, 0.9f, 0.9f, 0.8f);
        rightGrappleLine.material.SetFloat("_Metallic", 0.8f);
        rightGrappleLine.material.SetFloat("_Smoothness", 0.9f);
        rightGrappleLine.receiveShadows = true;
        rightGrappleLine.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        rightGrappleLine.positionCount = 2;
        rightGrappleLine.enabled = false;
    }

    public override void Disable()
    {
        base.Disable();
        if (leftGrappleJoint != null) Destroy(leftGrappleJoint);
        if (rightGrappleJoint != null) Destroy(rightGrappleJoint);
        leftGrappleLine.enabled = false;
        rightGrappleLine.enabled = false;
        isLeftGrappling = false;
        isRightGrappling = false;
    }

    private void UpdateGrapple(bool useRightHand, bool isGripHeld)
    {
        if (!modEnabled) return;

        if (useRightHand)
        {
            if (isGripHeld && GorillaTagger.Instance != null && GorillaTagger.Instance.rightHandTransform != null)
            {
                if (!isRightGrappling)
                {
                    Transform handTransform = GorillaTagger.Instance.rightHandTransform;
                    if (Physics.Raycast(handTransform.position, handTransform.forward, out RaycastHit hit, 100f))
                    {
                        rightGrapplePoint = hit.point;
                        rightGrappleJoint = GorillaTagger.Instance.gameObject.AddComponent<SpringJoint>();
                        rightGrappleJoint.autoConfigureConnectedAnchor = false;
                        rightGrappleJoint.connectedAnchor = rightGrapplePoint;
                        rightGrappleJoint.maxDistance = Vector3.Distance(handTransform.position, rightGrapplePoint) * 0.9f;
                        rightGrappleJoint.minDistance = 0.1f;
                        rightGrappleJoint.spring = 100f;
                        rightGrappleJoint.damper = 10f;
                        rightGrappleJoint.massScale = 2f;
                        rightGrappleLine.enabled = true;
                        isRightGrappling = true;
                    }
                }
            }
            else
            {
                if (rightGrappleJoint != null) Destroy(rightGrappleJoint);
                rightGrappleLine.enabled = false;
                isRightGrappling = false;
            }
        }
        else
        {
            if (isGripHeld && GorillaTagger.Instance != null && GorillaTagger.Instance.leftHandTransform != null)
            {
                if (!isLeftGrappling)
                {
                    Transform handTransform = GorillaTagger.Instance.leftHandTransform;
                    if (Physics.Raycast(handTransform.position, handTransform.forward, out RaycastHit hit, 100f))
                    {
                        leftGrapplePoint = hit.point;
                        leftGrappleJoint = GorillaTagger.Instance.gameObject.AddComponent<SpringJoint>();
                        leftGrappleJoint.autoConfigureConnectedAnchor = false;
                        leftGrappleJoint.connectedAnchor = leftGrapplePoint;
                        leftGrappleJoint.maxDistance = Vector3.Distance(handTransform.position, leftGrapplePoint) * 0.9f;
                        leftGrappleJoint.minDistance = 0.1f;
                        leftGrappleJoint.spring = 100f;
                        leftGrappleJoint.damper = 10f;
                        leftGrappleJoint.massScale = 2f;
                        leftGrappleLine.enabled = true;
                        isLeftGrappling = true;
                    }
                }
            }
            else
            {
                if (leftGrappleJoint != null) Destroy(leftGrappleJoint);
                leftGrappleLine.enabled = false;
                isLeftGrappling = false;
            }
        }
    }

    public override void OnUpdate()
    {
        bool leftGrab = ControllerInputPoller.instance != null && ControllerInputPoller.instance.leftGrab;
        bool rightGrab = ControllerInputPoller.instance != null && ControllerInputPoller.instance.rightGrab;

        UpdateGrapple(false, leftGrab);
        UpdateGrapple(true, rightGrab);

        if (isLeftGrappling && GorillaTagger.Instance != null && GorillaTagger.Instance.leftHandTransform != null)
        {
            leftGrappleLine.SetPosition(0, GorillaTagger.Instance.leftHandTransform.position);
            leftGrappleLine.SetPosition(1, leftGrapplePoint);
        }
        if (isRightGrappling && GorillaTagger.Instance != null && GorillaTagger.Instance.rightHandTransform != null)
        {
            rightGrappleLine.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
            rightGrappleLine.SetPosition(1, rightGrapplePoint);
        }
    }

    public override PageType pageType => PageType.Toggle;
}