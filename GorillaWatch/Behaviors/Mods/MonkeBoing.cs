using GorillaLocomotion;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class MonkeBoing : ModPage
    {
        public override string modName => "MonkeBoing";

        public override void Disable()
        {
            base.Disable();
            GTPlayer.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            GTPlayer.Instance.bodyCollider.material.bounciness = 0f;
        }

        public override void Enable()
        {
            base.Enable();
            GTPlayer.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            GTPlayer.Instance.bodyCollider.material.bounciness = 0.9f;
        }

        public override PageType pageType => PageType.Toggle;
    }
}