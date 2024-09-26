using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class BigMonkers : Page
    {
        public override string modName => "12) BigMonkers";

        public override void OnUpdate()
        {
            GorillaLocomotion.Player.Instance.scale = 2f;
        }
    }
}
