using GorillaLocomotion;
using TheGorillaWatch.Models;
using UnityEngine;
using System.Collections;

namespace TheGorillaWatch.Mods
{
    class Checkpoint : Page
    {
        public static GameObject CheckpointBox = null;
        private bool isTeleporting = false;

        public override string modName => "Checkpoint";

        public override void Disable()
        {
            base.Disable();
            if (CheckpointBox != null)
            {
                GameObject.Destroy(CheckpointBox);
                CheckpointBox = null;
            }
        }

        public override void Enable()
        {
            base.Enable();
            if (CheckpointBox == null)
            {
                CheckpointBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CheckpointBox.transform.position = new Vector3(0, 2, 0);
                CheckpointBox.transform.localScale = new Vector3(.2f, .2f, .2f);
                CheckpointBox.name = "CheckpointBox";
                GameObject.Destroy(CheckpointBox.GetComponent<BoxCollider>());

                UpdateCheckpointColor(Color.green);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (ControllerInputPoller.instance == null)
                return;

            if (ControllerInputPoller.instance.leftGrab && CheckpointBox != null)
            {
                CheckpointBox.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }

            if (ControllerInputPoller.instance.rightGrab && CheckpointBox != null && !isTeleporting)
            {
                TeleportToCheckpoint();
            }
        }

        private void TeleportToCheckpoint()
        {
            isTeleporting = true;
            UpdateCheckpointColor(Color.red);

            GorillaTagger.Instance.offlineVRRig.transform.position = CheckpointBox.transform.position;

            GorillaTagger.Instance.StartCoroutine(TeleportColorRoutine());
        }

        private IEnumerator TeleportColorRoutine()
        {
            yield return new WaitForSeconds(1);

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
