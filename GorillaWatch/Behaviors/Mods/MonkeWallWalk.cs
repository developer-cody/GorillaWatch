using TheGorillaWatch.Behaviors.Page;

namespace TheGorillaWatch.Behaviors.Mods
{
    class MonkeWallWalk : ModPage
    {
        public override string modName => "MonkeWallWalk";
        public static bool MonkeWallWalkEnabled;

        public override void Enable()
        {
            base.Enable();
            MonkeWallWalkEnabled = true;
        }

        public override void Disable()
        {
            base.Disable();
            MonkeWallWalkEnabled = false;
        }

        public override PageType pageType => PageType.Toggle;
    }
}
