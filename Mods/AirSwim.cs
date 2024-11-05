using GorillaLocomotion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class AirSwim : Page
    {
        public override string modName => "AirSwim";
        GameObject Swim = null;

        public override void Disable()
        {
            base.Disable();
            if (Swim != null)
            {
                Destroy(Swim);
            }
        }

        public override void Enable()
        {
            base.Enable();
            if (Swim == null)
            {
                Swim = Instantiate(GameObject.Find("CaveWaterVolume"));
                Swim.transform.localScale = new Vector3(5f, 1000f, 5f);
                Swim.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                Player.Instance.audioManager.UnsetMixerSnapshot(0.1f);
                Swim.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(0f, 200f, 0f);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Swim != null)
            {
                GorillaLocomotion.Player.Instance.audioManager.UnsetMixerSnapshot(0.1f);
                Swim.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(0f, 200f, 0f);
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}
