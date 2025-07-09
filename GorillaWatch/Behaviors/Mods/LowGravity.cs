using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using TheGorillaWatch.Utilities;

namespace TheGorillaWatch.Behaviors.Mods
{
    class LowGravity : ModPage
    {
        public override string modName => "LowGravity";
        public override List<string> incompatibleModNames => new List<string>() { "HighGravity", "NoGravity", "MonkeWallWalk" };

        public override void Enable()
        {
            base.Enable();
            GravityUtils.SetGravity(-4f);
        }

        public override void Disable()
        {
            base.Disable();
            GravityUtils.SetGravity(GravityUtils.ogGrav);
        }

        public override PageType pageType => PageType.Toggle;
    }
}