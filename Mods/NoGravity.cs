using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class NoGravity : Page
    {
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
