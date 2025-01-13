using System.Collections.Generic;
using TheGorillaWatch.Models;

namespace TheGorillaWatch.Mods
{
    class NoSlip : Page
    {
        public override string modName => "NoSlip";
        public override List<string> incompatibleModNames => new List<string> { "SlipMonk" };

        public static bool NoSlipEnabled;

        public override void Enable()
        {
            base.Enable();

            NoSlipEnabled = true;
        }

        public override void Disable()
        {
            base.Disable();

            NoSlipEnabled = false;
        }

        public override PageType pageType => PageType.Toggle;
    }
}