using System.Threading.Tasks;
using GorillaLocomotion;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class AirSwim : ModPage
    {
        public override string modName => "AirSwim";

        private GameObject waterTemplate;
        private GameObject airWaterZone;

        public override void Init()
        {
            base.Init();
            GorillaTagger.OnPlayerSpawned(async () =>
            {
                await Task.Delay(10000);

                GameObject caveWater = GameObject.Find("CaveWaterVolume");
                if (caveWater != null && waterTemplate == null)
                {
                    waterTemplate = Instantiate(caveWater);
                    waterTemplate.name = "AirSwim_WaterTemplate";
                    waterTemplate.transform.localScale = Vector3.one;
                    waterTemplate.GetComponent<Renderer>().enabled = false;
                    waterTemplate.SetActive(false);
                }
            });
        }

        public override void Enable()
        {
            base.Enable();

            if (waterTemplate == null) return;

            if (airWaterZone == null)
            {
                airWaterZone = Instantiate(waterTemplate);
                airWaterZone.name = "AirSwim_ActiveZone";
                airWaterZone.transform.localScale = new Vector3(5f, 1000f, 5f);
                airWaterZone.GetComponent<Renderer>().enabled = false;
                airWaterZone.SetActive(true);
            }

            Vector3 headPosition = GorillaTagger.Instance.headCollider.transform.position;
            airWaterZone.transform.position = headPosition + new Vector3(0f, 200f, 0f);
            GTPlayer.Instance.audioManager.UnsetMixerSnapshot(0.1f);
        }

        public override void Disable()
        {
            base.Disable();

            if (airWaterZone != null)
            {
                Destroy(airWaterZone);
                airWaterZone = null;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (airWaterZone != null)
            {
                Vector3 headPosition = GorillaTagger.Instance.headCollider.transform.position;
                airWaterZone.transform.position = headPosition + new Vector3(0f, 200f, 0f);
                GTPlayer.Instance.audioManager.UnsetMixerSnapshot(0.1f);
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}