
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class LowGravity : Page
    {
        public override string modName => "LowGravity";
        public override List<string> incompatibleModNames => new List<string>() { "NoGravity", "HighGravity" };

        public override void Enable()
        {
            base.Enable();
            Physics.gravity = new Vector3(0, -6.66f, 0);
        }

        public override void Disable()
        {
            base.Disable();
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }

        public override PageType pageType => PageType.Toggle;
    }
}
