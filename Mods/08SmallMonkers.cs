using GorillaLocomotion;
using GorillaNetworking;
using Photon.Pun;
using System.Collections.Generic;
using TheGorillaWatch.Configuration;
using TheGorillaWatch.Models;

namespace TheGorillaWatch.Mods
{
    class SmallMonkers : Page
    {
        public override string modName => "SmallMonkers";
        public override List<string> incompatibleModNames => new List<string>() { "BigMonkers" };

        public override void Enable()
        {
            base.Enable();
            var hash1 = new ExitGames.Client.Photon.Hashtable();
            hash1.AddOrUpdate("size", ConfigManager.smallMonkersSize.Value);
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
            ChangeScale(ConfigManager.smallMonkersSize.Value);
        }

        public static void ChangeScale(float scale)
        {
            typeof(Player).GetField("nativeScale", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(Player.Instance, scale);
        }

        public override PageType pageType => PageType.Toggle;
    }
}