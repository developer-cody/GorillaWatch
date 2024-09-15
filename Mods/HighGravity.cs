using System;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class HighGravity : Page
    {
        public override string modName => "HighGravity";
        public override List<string> incompatibleModNames => new List<string>() { "LowGravity", "NoGravity" };

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
            Physics.gravity = new Vector3(0, -14, 0);
        }
    }
}
