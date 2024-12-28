using System;
using TheGorillaWatch.Models;

namespace TheGorillaWatch.Mods
{
    class MainPage : Page
    {
        public override string modName => "GorillaWatchMainInfoPageWABSHUWAJSD";
        public override string info => $"\n<color=black>Gorilla</color>Watch!\nTime: {DateTime.Now:HH:mm:ss}";
        public override PageType pageType => PageType.Information;

        public override void OnUpdate()
        {
            base.OnUpdate();

            info = $" <color=black>Gorilla</color>Watch!\n{DateTime.Now:HH:mm:ss}";
        }
    }
}
