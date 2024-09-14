using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class MainPage : Page
    {
        public override string modName => "GorillaWatchMainInfoPage";

        public override string info => "GorillaWatch!\nMade by:\nCody, Ty\nAnd Wryser!";
        public override PageType pageType => PageType.Information;
    }
}
