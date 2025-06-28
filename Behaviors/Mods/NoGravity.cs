using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class NoGravity : ModPage
    {
        public override string modName => "NoGravity";
        public override List<string> incompatibleModNames => new List<string>() { "LowGravity", "HighGravity" };
        private float ogGrav = Physics.gravity.y;

        public override void Enable() => SetGravity(0f);
        public override void Disable() => SetGravity(ogGrav);
        private void SetGravity(float gravityValue) => Physics.gravity = new Vector3(0, gravityValue, 0);

        
        public override PageType pageType => PageType.Toggle;
    }
}