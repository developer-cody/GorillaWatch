using GorillaLocomotion;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class Checkpoint : Page
    {
        public static GameObject CheckpointBox = null;

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

            if (ControllerInputPoller.instance.rightGrab && CheckpointBox != null)
            {
                Player.Instance.bodyCollider.transform.position = CheckpointBox.transform.position;
            }
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
    }
}
