using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class NoGravity : Page
    {
        public override List<string> incompatibleModNames => new List<string>() { "LowGravity", "HighGravity" };

        public override string modName => "NoGravity";

        public override void Enable()
        {
            base.Enable();
            Physics.gravity = Vector3.zero;
        }
        
        public override void Disable()
        {
            base.Disable();
            Physics.gravity = new Vector3(0f, -9.807f, 0f);
        }

        public override PageType pageType => PageType.Toggle;
    }
}
