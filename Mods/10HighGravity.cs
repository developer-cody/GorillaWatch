using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class HighGravity : Page
    {
        private const float HighGravityValue = -14.5f;
        private const float DefaultGravityValue = -9.8f;

        public override List<string> incompatibleModNames => new List<string>() { "LowGravity", "NoGravity" };

        public override string modName => "HighGravity";

        public override void Enable()
        {
            base.Enable();
            SetGravity(HighGravityValue);
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