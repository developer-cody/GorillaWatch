using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Networking
{
    public class SizeNetworkingManager : MonoBehaviourPunCallbacks
    {
        public override void OnJoinedRoom()
        {
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                if (p.CustomProperties.TryGetValue("size", out object sizeValue) && sizeValue is float size)
                {
                    try
                    {
                        UpdatePlayerSize(p, size);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error updating size for player {p.NickName}: {e.Message}");
                    }
                }
            }
        }

        void UpdatePlayerSize(Player p, float size)
        {
            try
            {
                var rig = GorillaGameManager.instance.FindPlayerVRRig(p);
                if (rig != null) rig.transform.localScale = new Vector3(size, size, size);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in UpdatePlayerSize for player {p.NickName}: {e.Message}");
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            if (changedProps.ContainsKey("size") && !targetPlayer.IsLocal)
            {
                try
                {
                    UpdatePlayerSize(targetPlayer, (float)changedProps["size"]);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error updating size for player {targetPlayer.NickName}: {e.Message}");
                }
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            try
            {
                var rig = GorillaGameManager.instance.FindPlayerVRRig(otherPlayer);
                if (rig != null) rig.transform.localScale = Vector3.one;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error resetting rig scale for player {otherPlayer.NickName}: {e.Message}");
            }
        }

        public override void OnLeftRoom()
        {
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                try
                {
                    var rig = GorillaGameManager.instance.FindPlayerVRRig(p);
                    if (rig != null) rig.transform.localScale = Vector3.one;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error resetting rig scale in OnLeftRoom: {e.Message}");
                }
            }
        }
    }
}
