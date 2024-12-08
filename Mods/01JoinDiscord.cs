using System.Diagnostics;
using TheGorillaWatch.Models;

namespace TheGorillaWatch.Mods
{
    class JoinDiscord : Page
    {
        public override string modName => "Join Discord";

        public override void Enable()
        {
            base.Enable();
            Process.Start("https://discord.gg/E7kTnTYZEG");
        }

        public override PageType pageType => PageType.Toggle;
    }
}
