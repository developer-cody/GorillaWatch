﻿using TheGorillaWatch.Models;

namespace TheGorillaWatch.Mods
{
    class MadeByInfo : Page
    {
        public override string modName => "MadeByInfo";
        public override string info => "Made By:\n<color=red>Cody</color>!";

        public override PageType pageType => PageType.Information;
    }
}