using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;

namespace TheGorillaWatch.Behaviors.Mods
{
    class SlipMonk : ModPage
    {
        public override string modName => "SlipMonk";
        public override List<string> incompatibleModNames => new List<string> { "NoSlip" };
        public static bool SlipMonkEnabled;

        public override void Enable()
        {
            base.Enable();
            SlipMonkEnabled = true;
        }

        public override void Disable()
        {
            base.Disable();
            SlipMonkEnabled = false;
        }

        public override PageType pageType => PageType.Toggle;
    }
}