using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class SpeedyMonk : Page
    {
        public override string modName => "SpeedyMonk";

        public override void OnUpdate()
        {
            base.OnUpdate();
            GorillaLocomotion.Player.Instance.jumpMultiplier = 1.3f * GorillaLocomotion.Player.Instance.scale;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 8.5f * GorillaLocomotion.Player.Instance.scale;
        }
        
        public override void Disable()
        {
            base.Disable();
            GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
        }
    }
}
