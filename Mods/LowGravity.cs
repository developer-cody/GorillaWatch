using System;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class LowGravity : Page
    {
        public override string modName => "LowGravity";
        public override List<string> incompatibleModNames => new List<string>() { "HighGravity", "NoGravity" };

        Vector3 ogGravity;
        public override void Init()
        {
            base.Init();
            ogGravity = Physics.gravity;
        }

        public override void Disable()
        {
            base.Disable();
            Physics.gravity = ogGravity;
        }

        public override void Enable()
        {
            base.Enable();
            Physics.gravity = new Vector3(0, -4, 0);
        }
    }
}
