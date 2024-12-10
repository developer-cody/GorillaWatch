using GorillaLocomotion;
using TheGorillaWatch.Models;

namespace TheGorillaWatch.Mods
{
    class SpeedyMonk : Page
    {
        // Default values
        private float DefaultJumpMultiplier;
        private float DefaultMaxJumpSpeed;

        // Modified values
        private const float ModifiedJumpMultiplier = 1.3f;
        private const float ModifiedMaxJumpSpeed = 8.5f;

        public override string modName => "SpeedyMonk";

        public override void Init()
        {
            base.Init();

            DefaultJumpMultiplier = Player.Instance.jumpMultiplier;
            DefaultMaxJumpSpeed = Player.Instance.maxJumpSpeed;
        }

        public override void Enable()
        {
            base.Enable();

            Player.Instance.jumpMultiplier = ModifiedJumpMultiplier;
            Player.Instance.maxJumpSpeed = ModifiedMaxJumpSpeed;
        }

        public override void Disable()
        {
            base.Disable();

            Player.Instance.jumpMultiplier = DefaultJumpMultiplier;
            Player.Instance.maxJumpSpeed = DefaultMaxJumpSpeed;
        }

        public override PageType pageType => PageType.Toggle;
    }
}
