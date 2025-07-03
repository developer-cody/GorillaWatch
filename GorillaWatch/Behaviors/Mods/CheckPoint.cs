using System.Collections;
using GorillaLocomotion;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class Checkpoint : ModPage
    {
        public override string modName => "Checkpoint";

        private bool isTeleporting = false;
        private float actionCooldownTime = 0.35f;
        private float lastActionTime = 0f;
        public static GameObject CheckpointBox = null;

        public override void Enable()
        {
            base.Enable();
            if (CheckpointBox == null)
            {
                CheckpointBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CheckpointBox.transform.position = Vector3.zero;
                CheckpointBox.transform.localScale = new Vector3(.2f, .2f, .2f);
                CheckpointBox.name = "CheckpointBox";
                Destroy(CheckpointBox.GetComponent<BoxCollider>());
                UpdateCheckpointColor(Color.green);
            }
        }

        public override void Disable()
        {
            base.Disable();
            if (CheckpointBox != null)
            {
                Destroy(CheckpointBox);
                CheckpointBox = null;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (ControllerInputPoller.instance == null) return;

            if (ControllerInputPoller.instance.leftGrab && CheckpointBox != null)
            {
                CheckpointBox.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }

            if (ControllerInputPoller.instance.rightGrab && CheckpointBox != null && !isTeleporting && CheckpointBox.transform.position != Vector3.zero)
            {
                if (Time.time - lastActionTime >= actionCooldownTime)
                {
                    isTeleporting = true;
                    UpdateCheckpointColor(Color.red);

                    GTPlayer.Instance.TeleportTo(CheckpointBox.transform);
                    GorillaTagger.Instance.StartCoroutine(TeleportColorRoutine());
                }
            }
        }

        private IEnumerator TeleportColorRoutine()
        {
            yield return new WaitForSeconds(.35f);

            UpdateCheckpointColor(Color.green);
            isTeleporting = false;
        }

        private void UpdateCheckpointColor(Color color)
        {
            if (CheckpointBox != null)
            {
                var renderer = CheckpointBox.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.material.shader = Shader.Find("GorillaTag/UberShader");
                    renderer.material.color = color;
                }
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}