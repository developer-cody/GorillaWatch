using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGorillaWatch.Models
{
    public class NetworkingManager : MonoBehaviourPunCallbacks
    {
        public Dictionary<Player, GameObject> PlayerList = new Dictionary<Player, GameObject>();
        public Dictionary<Player, GameObject> Riglist = new Dictionary<Player, GameObject>();

        public override void OnJoinedRoom()
        {
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                HandlePlayerInitialization(p);
            }
        }

        // Helper method to handle player initialization
        private void HandlePlayerInitialization(Player p)
        {
            if (p.CustomProperties.ContainsKey("GorillaWatch"))
            {
                StartCoroutine(nameof(WaitForInit), p);
            }

            if (p.CustomProperties.TryGetValue("size", out object sizeValue) && sizeValue is float size)
            {
                UpdatePlayerSize(p, size);
            }
        }

        // Helper method to update player size
        private void UpdatePlayerSize(Player p, float size)
        {
            var rig = GorillaGameManager.instance.FindPlayerVRRig(p);
            if (rig != null)
            {
                rig.transform.localScale = new Vector3(size, size, size);
            }
            else
            {
                Debug.LogWarning($"Could not find VR rig for player: {p.NickName}");
            }
        }

        IEnumerator WaitForInit(Player p)
        {
            yield return new WaitForSeconds(0.7f);
            InitializePlayerWatch(p);
        }

        // Helper method to initialize the player's watch
        private void InitializePlayerWatch(Player p)
        {
            var rig = GorillaGameManager.instance.FindPlayerVRRig(p);
            if (rig == null) return;

            var huntwatch = GameObject.Instantiate(GorillaTagger.Instance.offlineVRRig.huntComputer);
            huntwatch.transform.SetParent(rig.leftHandTransform, false);
            huntwatch.transform.localPosition = new Vector3(-0.6364f, 0.6427f, 0.0153f);
            huntwatch.transform.localRotation = GorillaTagger.Instance.offlineVRRig.huntComputer.transform.localRotation;

            PlayerList.Add(p, huntwatch);
            Riglist.Add(p, rig.gameObject);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (newPlayer.CustomProperties.ContainsKey("GorillaWatch"))
            {
                InitializePlayerWatch(newPlayer);
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            try
            {
                if (changedProps.ContainsKey("size") && !targetPlayer.IsLocal)
                {
                    UpdatePlayerSize(targetPlayer, (float)changedProps["size"]);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error with player {targetPlayer.NickName}: {e.Message}");
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (PlayerList.TryGetValue(otherPlayer, out var playerObject) && Riglist.TryGetValue(otherPlayer, out var rig))
            {
                CleanUpPlayer(playerObject, rig, otherPlayer);
            }
        }

        // Helper method to clean up when a player leaves
        private void CleanUpPlayer(GameObject playerObject, GameObject rig, Player player)
        {
            if (rig != null)
            {
                rig.transform.localScale = Vector3.one;
            }

            Destroy(playerObject);
            PlayerList.Remove(player);
            Riglist.Remove(player);
        }

        public override void OnLeftRoom()
        {
            foreach (var rig in Riglist.Values)
            {
                if (rig != null)
                {
                    rig.transform.localScale = Vector3.one;
                }
            }

            foreach (var watch in PlayerList.Values)
            {
                if (watch != null)
                {
                    Destroy(watch);
                }
            }

            PlayerList.Clear();
            Riglist.Clear();
        }
    }
}