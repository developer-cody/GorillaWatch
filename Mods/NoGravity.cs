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

        private bool isEnabled = false;

        public override void OnUpdate()
        {
            if (isEnabled)
            {
                Physics.gravity = Vector3.zero;
            }
            else
            {
                Physics.gravity = new Vector3(0, -9.81f, 0);
            }
        }

        public void ToggleGravity()
        {
            isEnabled = !isEnabled;
        }
    }
}
