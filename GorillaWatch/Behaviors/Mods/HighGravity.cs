﻿using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using TheGorillaWatch.Utilities;

namespace TheGorillaWatch.Behaviors.Mods
{
    class HighGravity : ModPage
    {
        public override string modName => "HighGravity";
        public override List<string> incompatibleModNames => new List<string>() { "LowGravity", "NoGravity", "MonkeWallWalk" };

        public override void Enable()
        {
            base.Enable();
            GravityUtils.SetGravity(-14f);
        }

        public override void Disable()
        {
            base.Disable();
            GravityUtils.SetGravity(GravityUtils.ogGrav);
        }

        public override PageType pageType => PageType.Toggle;
    }
}