using GorillaLocomotion;
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
            Player.Instance.jumpMultiplier = 1.3f;
            Player.Instance.maxJumpSpeed = 8.5f;
        }
        
        public override void Disable()
        {
            base.Disable();
            Player.Instance.jumpMultiplier = 1.1f;
            Player.Instance.maxJumpSpeed = 6.5f;
        }

        public override PageType pageType => PageType.Toggle;
    }
}
