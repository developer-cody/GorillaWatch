using GorillaLocomotion;
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
            bounce = Player.Instance.bodyCollider.material.bounciness;
            PMCombine = Player.Instance.bodyCollider.material.bounceCombine;
            Player.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            Player.Instance.bodyCollider.material.bounciness = 0f;
        }

        public override void Enable()
        {
            base.Enable();
            bounce = Player.Instance.bodyCollider.material.bounciness;
            PMCombine = Player.Instance.bodyCollider.material.bounceCombine;
            Player.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            Player.Instance.bodyCollider.material.bounciness = .75f;
        }
        public override PageType pageType => PageType.Toggle;

    }
}
