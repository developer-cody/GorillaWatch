using GorillaLocomotion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

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
