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

        public override void Enable()
        {
            base.Enable();
            GorillaLocomotion.Player.Instance.jumpMultiplier = 1.2f;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 8f;
        }
        
        public override void Disable()
        {
            base.Disable();
            GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
        }
    }
}
