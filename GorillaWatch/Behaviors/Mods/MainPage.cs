using System;
using TheGorillaWatch.Behaviors.Page;

namespace TheGorillaWatch.Behaviors.Mods
{
    class MainPage : ModPage
    {
        public override string modName => "MainPage";
        public override string info => $"\n<color=black>Gorilla</color>Watch\nTime: {DateTime.Now:HH:mm:ss}";

        public override void OnUpdate() => info = $" <color=black>Gorilla</color>Watch!\n{DateTime.Now:HH:mm:ss}";

        public override PageType pageType => PageType.Information;
    }
}
