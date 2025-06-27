using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class MonkeWalker : ModPage
    {
        public override string modName => "MonkeWalker";
        private GameObject playerColliderParent;

        public override void Disable()
        {
            base.Disable();

            if (playerColliderParent != null)
            {
                Destroy(playerColliderParent);
            }
        }

        public override void OnUpdate()
        {
            if (playerColliderParent == null)
            {
                playerColliderParent = new GameObject("PlayerCollidersParent");
            }
            else
            {
                foreach (Transform child in playerColliderParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (VRRig vrig in GorillaParent.instance.vrrigs)
            {
                if (vrig == GorillaTagger.Instance.offlineVRRig) continue;

                CreateColliderForVRig(vrig);
            }
        }

        private void CreateColliderForVRig(VRRig vrig)
        {
            GameObject playerCollider = GameObject.CreatePrimitive(PrimitiveType.Cube);
            playerCollider.transform.SetParent(playerColliderParent.transform, false);
            playerCollider.transform.position = vrig.transform.position;
            playerCollider.transform.rotation = vrig.transform.rotation;
            playerCollider.transform.localScale = new Vector3(0.3f, 0.55f, 0.3f);

            playerCollider.GetComponent<Renderer>().enabled = false;

            BoxCollider collider = playerCollider.GetComponent<BoxCollider>();
            if (collider == null) collider = playerCollider.AddComponent<BoxCollider>();
            collider.isTrigger = false;

            Rigidbody rb = playerCollider.GetComponent<Rigidbody>();
            if (rb == null) rb = playerCollider.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            Rigidbody vrigRb = vrig.gameObject.GetComponent<Rigidbody>();
            if (vrigRb != null)
            {
                vrigRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}