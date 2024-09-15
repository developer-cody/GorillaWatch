using System;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class NoGravity : Page
    {
        public override string modName => "NoGravity";
        public override List<string> incompatibleModNames => new List<string>() { "LowGravity", "HighGravity" };

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
            Physics.gravity = Vector3.zero;
        }
    }
}
