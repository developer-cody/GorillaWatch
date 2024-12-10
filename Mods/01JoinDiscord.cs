using System.Diagnostics;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class JoinDiscord : Page
    {
        public override string modName => "Join Discord";
        string DiscordInvite = "https://discord.gg/E7kTnTYZEG";

        public override void Enable()
        {
            base.Enable();

            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                Process.Start(DiscordInvite);
            }
            else
            {
                Application.OpenURL(DiscordInvite);
            }
        }

        public override PageType pageType => PageType.Toggle;
    }
}