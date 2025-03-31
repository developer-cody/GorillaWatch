using TheGorillaWatch.Models;

namespace TheGorillaWatch.Mods
{
    class WallRun : Page
    {
        public override string modName => "MonkeWallWalk";
        public override PageType pageType => PageType.Toggle;
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
    }
}
