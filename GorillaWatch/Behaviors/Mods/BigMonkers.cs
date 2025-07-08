using System.Collections.Generic;
using GorillaLocomotion;
using GorillaNetworking;
using Photon.Pun;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    class BigMonkers : ModPage
    {
        public override string modName => "BigMonkers";
        public override List<string> incompatibleModNames => new List<string>() { "SmallMonkers" };

        private Vector3 grav = Physics.gravity / ConfigManager.bigMonkersSize.Value;

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

            typeof(GTPlayer).GetField("nativeScale", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GTPlayer.Instance, 1f);
            Physics.gravity = new Vector3(0, GravityUtils.ogGrav, 0);
        }

        public override void OnUpdate()
        {
            typeof(GTPlayer).GetField("nativeScale", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GTPlayer.Instance, ConfigManager.bigMonkersSize.Value);
            Physics.gravity = grav;
        }

        public override PageType pageType => PageType.Toggle;
    }
}