using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class MonkeBoing : Page
    {
        public override string modName => "MonkeBoing";

        float bounce;
        PhysicMaterialCombine PMCombine;

        public override void Disable()
        {
            base.Disable();
            bounce = GorillaLocomotion.Player.Instance.bodyCollider.material.bounciness;
            PMCombine = GorillaLocomotion.Player.Instance.bodyCollider.material.bounceCombine;
            GorillaLocomotion.Player.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            GorillaLocomotion.Player.Instance.bodyCollider.material.bounciness = 0f;
        }

        public override void Enable()
        {
            base.Enable();
            bounce = GorillaLocomotion.Player.Instance.bodyCollider.material.bounciness;
            PMCombine = GorillaLocomotion.Player.Instance.bodyCollider.material.bounceCombine;
            GorillaLocomotion.Player.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            GorillaLocomotion.Player.Instance.bodyCollider.material.bounciness = 1.0f;
        }
    }
}
