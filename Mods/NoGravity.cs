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

        public override void OnUpdate()
        {
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(Vector3.up * 9.8f, ForceMode.Acceleration);
        }
    }
}
