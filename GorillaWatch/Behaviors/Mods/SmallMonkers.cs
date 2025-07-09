using GorillaLocomotion;
using GorillaNetworking;
using Photon.Pun;
using System.Collections.Generic;
using TheGorillaWatch.Behaviors.Page;
using TheGorillaWatch.Utilities;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class SmallMonkers : ModPage
    {
        public override string modName => "SmallMonkers";
        public override List<string> incompatibleModNames => new List<string>() { "BigMonkers" };

        private Vector3 grav = Physics.gravity / ConfigManager.smallMonkersSize.Value;

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

            typeof(GTPlayer).GetField("nativeScale", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GTPlayer.Instance, 1f);
            Physics.gravity = new Vector3(0, GravityUtils.ogGrav, 0);
        }

        public override void OnUpdate() 
        {
            typeof(GTPlayer).GetField("nativeScale", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GTPlayer.Instance, ConfigManager.smallMonkersSize.Value);
            Physics.gravity = grav;
        }

        public override PageType pageType => PageType.Toggle;
    }
}