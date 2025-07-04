using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    internal class BananaSpawner : ModPage
    {
        public override string modName => "BananaSpawner";

        private GameObject baseBanana;
        private bool wasGrabbing;

        public override PageType pageType => PageType.Toggle;

        public override void Init()
        {
            base.Init();

            AssetBundle bundle = Utils.LoadAssetBundleFromURL("https://github.com/developer-cody/GorillaWatch/raw/refs/heads/main/Assets/banana");
            if (bundle != null) baseBanana = bundle.LoadAsset<GameObject>("banana");
            else Debug.LogError("Failed to load banana asset bundle.");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (baseBanana == null) return;

            bool grabbing = ControllerInputPoller.instance.rightGrab;

            if (grabbing && !wasGrabbing)
            {
                GameObject banana = Instantiate(baseBanana, GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position, Quaternion.identity);

                banana.layer = 8;

                if (!banana.TryGetComponent<Rigidbody>(out _)) banana.AddComponent<Rigidbody>();
            }

            wasGrabbing = grabbing;
        }
    }
}