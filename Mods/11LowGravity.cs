using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class LowGravity : Page
    {
        public override string modName => "LowGravity";
        public override List<string> incompatibleModNames => new List<string>() { "NoGravity", "HighGravity" };

        private const float LowGravityValue = -6.66f;
        private const float DefaultGravityValue = -9.8f;

        public override void Enable()
        {
            base.Enable();
            SetGravity(LowGravityValue);
        }

        public override void Disable()
        {
            base.Disable();
            SetGravity(DefaultGravityValue);
        }

        private void SetGravity(float gravityValue)
        {
            Physics.gravity = new Vector3(0, gravityValue, 0);
        }

        public override PageType pageType => PageType.Toggle;
    }
}
