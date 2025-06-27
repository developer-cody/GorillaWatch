using GorillaLocomotion;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class MonkePunch : ModPage
    {
        public override string modName => "MonkePunch";

        public static Vector3[] lastLeft = new Vector3[10];
        public static Vector3[] lastRight = new Vector3[10];

        public override void OnUpdate()
        {
            int index = -1;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    index++;

                    Vector3 rightHandPos = vrrig.rightHandTransform.position;
                    Vector3 playerHeadPos = GorillaTagger.Instance.offlineVRRig.head.rigTarget.position;
                    float rightDistance = Vector3.Distance(rightHandPos, playerHeadPos);

                    if (rightDistance < .3f)
                    {
                        GTPlayer.Instance.GetComponent<Rigidbody>().velocity +=
                            Vector3.Normalize(rightHandPos - lastRight[index]) * 5f;
                    }
                    lastRight[index] = rightHandPos;

                    Vector3 leftHandPos = vrrig.leftHandTransform.position;
                    float leftDistance = Vector3.Distance(leftHandPos, playerHeadPos);

                    if (leftDistance < .3f)
                    {
                        GTPlayer.Instance.GetComponent<Rigidbody>().velocity +=
                            Vector3.Normalize(leftHandPos - lastLeft[index]) * 5f;
                    }
                    lastLeft[index] = leftHandPos;
                }
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}