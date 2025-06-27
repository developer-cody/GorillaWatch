using GorillaLocomotion;
using GorillaNetworking;
using Photon.Pun;
using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using TheGorillaWatch.Configuration;

namespace TheGorillaWatch.Behaviors.Mods
{
    class BigMonkers : ModPage
    {
        public override string modName => "BigMonkers";
        public override List<string> incompatibleModNames => new List<string>() { "SmallMonkers" };
        public override void Enable()
        {
            base.Enable();
            var hash1 = new ExitGames.Client.Photon.Hashtable();
            hash1.AddOrUpdate("size", ConfigManager.bigMonkersSize.Value);
            PhotonNetwork.SetPlayerCustomProperties(hash1);
        }

        public override void Disable()
        {
            base.Disable();
            var hash2 = new ExitGames.Client.Photon.Hashtable();
            hash2.AddOrUpdate("size", 1f);
            PhotonNetwork.SetPlayerCustomProperties(hash2);

            ChangeScale(1f);
        }

        public override void OnUpdate()
        {
            ChangeScale(ConfigManager.bigMonkersSize.Value);
        }

        public static void ChangeScale(float scale)
        {
            typeof(GTPlayer).GetField("nativeScale", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GTPlayer.Instance, scale);
        }

        public override PageType pageType => PageType.Toggle;
    }
}