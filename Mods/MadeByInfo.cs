﻿using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class MadeByInfo : Page
    {
        public override string modName => "MadeByInfo";

        public override string info => "Made By: <color=yellow>Cody</color> <color=blue>Wryser</color> <color=red>Striker</color> <color=E6E6FA>Socks</color>!";

        public override PageType pageType => PageType.Information;
    }
}
