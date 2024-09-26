using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class SmallMonkers : Page
    {
        public override string modName => "13) SmallMonkers";

        public override void OnUpdate()
        {
            GorillaLocomotion.Player.Instance.scale = .2f;
        }
    }
}
