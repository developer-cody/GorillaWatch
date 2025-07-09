using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using TheGorillaWatch.Utilities;

namespace TheGorillaWatch.Behaviors.Mods
{
    class NoGravity : ModPage
    {
        public override string modName => "NoGravity";
        public override List<string> incompatibleModNames => new List<string>() { "HighGravity", "LowGravity", "MonkeWallWalk" };

        public override void Enable()
        {
            base.Enable();
            GravityUtils.SetGravity(0f);
        }

        public override void Disable()
        {
            base.Disable();
            GravityUtils.SetGravity(GravityUtils.ogGrav);
        }

        public override PageType pageType => PageType.Toggle;
    }
}