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
    class BigMonkers : Page
    {
        public override string modName => "BigMonkers";

        public override void Enable()
        {
            base.Enable();
            var hash1 = new ExitGames.Client.Photon.Hashtable();
            hash1.AddOrUpdate("size", ConfigManager.BigMonkersSize.Value);
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
            GorillaLocomotion.Player.Instance.scale = ConfigManager.BigMonkersSize.Value;
        }
        public override PageType pageType => PageType.Toggle;

    }
}
