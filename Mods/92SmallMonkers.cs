using GorillaLocomotion;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Configuration;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class SmallMonkers : Page
    {
        public override string modName => "SmallMonkers";

        public override void Enable()
        {
            base.Enable();
            var hash1 = new ExitGames.Client.Photon.Hashtable();
            hash1.AddOrUpdate("size", ConfigManager.SmallMonkersSize.Value);
            PhotonNetwork.SetPlayerCustomProperties(hash1);
        }
        public override void Disable()
        {
            base.Disable();
            var hash2 = new ExitGames.Client.Photon.Hashtable();
            hash2.AddOrUpdate("size", 1f);
            PhotonNetwork.SetPlayerCustomProperties(hash2);
        }
        public override void OnUpdate()
        {
            Player.Instance.scale = ConfigManager.SmallMonkersSize.Value;
        }
    }
}
