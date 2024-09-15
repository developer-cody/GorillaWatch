using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class SmallMonkers : Page
    {
        public override string modName => "SmallMonkers";
        public override List<string> incompatibleModNames => new List<string>() { "BigMonkers" };

        public override void OnUpdate()
        {
            GorillaLocomotion.Player.Instance.scale = .5f;
        }
    }
}
