using GorillaLocomotion;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class BigMonkers : Page
    {
        public override string modName => "BigMonkers";

        public override void Enable()
        {
            var hash1 = new ExitGames.Client.Photon.Hashtable();
            hash1.AddOrUpdate("size", 2f);
            PhotonNetwork.SetPlayerCustomProperties(hash1);
        }
        public override void Disable()
        {
            var hash2 = new ExitGames.Client.Photon.Hashtable();
            hash2.AddOrUpdate("size", 1);
            PhotonNetwork.SetPlayerCustomProperties(hash2);
        }
        public override void OnUpdate()
        {
            Player.Instance.scale = 2f;
        }
    }
}
