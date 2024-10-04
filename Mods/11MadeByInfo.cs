using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class MadeByInfo : Page
    {
        public override string modName => "MadeByInfo";

        public override string info => "Made By:\n<color=yellow>Cody</color>, <color=red>Ty</color>,<color=blue>Wryser</color>, and <color=red>Striker</color>!";

        public override PageType pageType => PageType.Information;
    }
}
