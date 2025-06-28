using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Behaviors.Page;

namespace TheGorillaWatch.Behaviors.Mods
{
    class MadeBy : ModPage
    {
        public override string modName => "MadeBy";
        public override string info => "<color=blue>cody</color>, <color=red>Ty</color>, <color=#00FFFF>wryser</color>, <color=red>Striker</color>";
        public override PageType pageType => PageType.Information;
    }
}
