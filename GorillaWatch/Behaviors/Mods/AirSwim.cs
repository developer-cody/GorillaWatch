using System.Threading.Tasks;
using GorillaLocomotion;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class AirSwim : ModPage
    {
        public override string modName => "AirSwim";

        GameObject Base;
        GameObject Swim;

        public async override void Init()
        {
            base.Init();
            GorillaTagger.OnPlayerSpawned(async () =>
            {
                await Task.Delay(10000);
                if (Base == null)
                {
                    Base = Instantiate(GameObject.Find("CaveWaterVolume"));
                    Base.transform.localScale = Vector3.one;
                    Base.GetComponent<Renderer>().enabled = false;
                }
            });
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Swim != null)
            {
                GTPlayer.Instance.audioManager.UnsetMixerSnapshot(0.1f);
                Swim.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(0f, 200f, 0f);
            }
        }

        public override void Enable()
        {
            base.Enable();
            if (Swim == null)
            {
                Swim = Instantiate(Base);
                Swim.transform.localScale = new Vector3(5f, 1000f, 5f);
                Swim.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                GTPlayer.Instance.audioManager.UnsetMixerSnapshot(0.1f);
                Swim.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(0f, 200f, 0f);
            }
        }

        public override void Disable()
        {
            base.Disable();
            if (Swim != null)
            {
                Destroy(Swim);
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}