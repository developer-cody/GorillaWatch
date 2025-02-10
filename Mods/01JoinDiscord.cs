using System.Diagnostics;
using System.Threading.Tasks;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class JoinDiscord : Page
    {
        public override string modName => "Join <color=blue>Discord</color>!";
        public override string info => "Click <color=red>LEFT JOYSTICK</color> to join!";
        private readonly string DiscordInvite = "https://discord.gg/jsyWpJEkQ6";
        private static bool isDiscordOpened = false;

        public async override void OnUpdate()
        {
            base.OnUpdate();

            if (!isDiscordOpened)
            {
                isDiscordOpened = true;

                if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    Process.Start(DiscordInvite);
                }
                else
                {
                    Application.OpenURL(DiscordInvite);
                }

                await Task.Delay(100);
                isDiscordOpened = false;
            }
        }

        public override PageType pageType => PageType.notatogglebutnotinfo;
    }
}