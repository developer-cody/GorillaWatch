using GorillaLocomotion;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class MonkeBoing : ModPage
    {
        public override string modName => "MonkeBoing";

        float bounce;
        PhysicMaterialCombine PMCombine;

        public override void Disable()
        {
            base.Disable();
            bounce = GTPlayer.Instance.bodyCollider.material.bounciness;
            PMCombine = GTPlayer.Instance.bodyCollider.material.bounceCombine;
            GTPlayer.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            GTPlayer.Instance.bodyCollider.material.bounciness = 0f;
        }

        public override void Enable()
        {
            base.Enable();
            bounce = GTPlayer.Instance.bodyCollider.material.bounciness;
            PMCombine = GTPlayer.Instance.bodyCollider.material.bounceCombine;
            GTPlayer.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            GTPlayer.Instance.bodyCollider.material.bounciness = 0.95f;
        }

        public override PageType pageType => PageType.Toggle;
    }
}