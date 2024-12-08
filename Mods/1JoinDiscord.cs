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
            Process.Start("https://discord.gg/qBmHgKmNMZ");
        }

        public override PageType pageType => PageType.Toggle;
    }
}
