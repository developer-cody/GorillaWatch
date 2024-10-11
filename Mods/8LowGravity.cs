using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class LowGravity : Page
    {
        public override string modName => "LowGravity";

        public override void Enable()
        {
            base.Enable();
            Physics.gravity = new Vector3(0, -6.66f, 0);
        }

        public override void Disable()
        {
            base.Disable();
            Physics.gravity = new Vector3(0, -9.807f, 0);
        }
        public override PageType pageType => PageType.Toggle;

    }
}
