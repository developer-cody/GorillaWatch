using TheGorillaWatch.Models;
using UnityEngine;
using System.Collections;
using System;

namespace TheGorillaWatch.Mods
{
    class Checkpoint : Page
    {
        public static GameObject CheckpointBox = null;
        private bool isTeleporting = false;
        private bool doActionPerformed = false;
        private float actionCooldownTime = 0.2f;
        private float lastActionTime = 0f;

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

            if (ControllerInputPoller.instance.rightGrab && CheckpointBox != null && !isTeleporting && CheckpointBox.transform.position != Vector3.zero)
            {
                if (Time.time - lastActionTime >= actionCooldownTime)
                {
                    TeleportToCheckpoint();

                    if (!doActionPerformed)
                    {
                        try
                        {
                            MeshCollider[] array = FindObjectsOfType<MeshCollider>();
                            foreach (MeshCollider meshCollider in array)
                            {
                                meshCollider.enabled = false;
                            }
                            doActionPerformed = true;
                            lastActionTime = Time.time;
                        }
                        catch (Exception e)
                        {
                            Debug.Log($"Error with Noclip with Checkpoint: {e.Message}");
                        }
                    }
                }
            }
            else if (doActionPerformed && Time.time - lastActionTime >= actionCooldownTime)
            {
                try
                {
                    MeshCollider[] array = FindObjectsOfType<MeshCollider>();
                    foreach (MeshCollider meshCollider in array)
                    {
                        meshCollider.enabled = true;
                    }
                    doActionPerformed = false;
                    lastActionTime = Time.time;
                }
                catch (Exception e)
                {
                    Debug.Log($"Error with Noclip Disable: {e.Message}");
                }
            }
        }

        private void TeleportToCheckpoint()
        {
            isTeleporting = true;
            UpdateCheckpointColor(Color.red);

            GorillaLocomotion.Player.Instance.headCollider.transform.position = CheckpointBox.transform.position;

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