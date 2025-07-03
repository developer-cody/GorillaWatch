using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace TheGorillaWatch.Behaviors
{
    public class NetworkingManager : MonoBehaviourPunCallbacks
    {
        public Dictionary<Player, GameObject> PlayerList = new Dictionary<Player, GameObject>();
        public Dictionary<Player, GameObject> Riglist = new Dictionary<Player, GameObject>();

        public override void OnJoinedRoom()
        {
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                try
                {
                    HandlePlayerInitialization(p);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error while handling player initialization for {p.NickName}: {e.Message}");
                }
            }
        }

        private void HandlePlayerInitialization(Player p)
        {
            if (p.CustomProperties.ContainsKey("GorillaWatch"))
            {
                try
                {
                    StartCoroutine(nameof(WaitForInit), p);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error starting WaitForInit coroutine for player {p.NickName}: {e.Message}");
                }
            }

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

        private void UpdatePlayerSize(Player p, float size)
        {
            try
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
            catch (Exception e)
            {
                Debug.LogError($"Error in UpdatePlayerSize for player {p.NickName}: {e.Message}");
            }
        }

        IEnumerator WaitForInit(Player p)
        {
            yield return new WaitForSeconds(0.7f);

            try
            {
                InitializePlayerWatch(p);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error initializing watch for player {p.NickName}: {e.Message}");
            }
        }

        private void InitializePlayerWatch(Player p)
        {
            try
            {
                var rig = GorillaGameManager.instance.FindPlayerVRRig(p);
                if (rig == null) return;

                var huntwatch = Instantiate(GorillaTagger.Instance.offlineVRRig.huntComputer);
                huntwatch.transform.SetParent(rig.leftHandTransform, false);
                huntwatch.transform.localPosition = new Vector3(-0.6364f, 0.6427f, 0.0153f);
                huntwatch.transform.localRotation = GorillaTagger.Instance.offlineVRRig.huntComputer.transform.localRotation;

                PlayerList.Add(p, huntwatch);
                Riglist.Add(p, rig.gameObject);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in InitializePlayerWatch for player {p.NickName}: {e.Message}");
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (newPlayer.CustomProperties.ContainsKey("GorillaWatch"))
            {
                try
                {
                    InitializePlayerWatch(newPlayer);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error initializing player watch for {newPlayer.NickName}: {e.Message}");
                }
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
            if (PlayerList.TryGetValue(otherPlayer, out var playerObject) && Riglist.TryGetValue(otherPlayer, out var rig))
            {
                try
                {
                    CleanUpPlayer(playerObject, rig, otherPlayer);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error cleaning up player {otherPlayer.NickName}: {e.Message}");
                }
            }
        }

        private void CleanUpPlayer(GameObject playerObject, GameObject rig, Player player)
        {
            try
            {
                if (rig != null)
                {
                    rig.transform.localScale = Vector3.one;
                }

                Destroy(playerObject);
                PlayerList.Remove(player);
                Riglist.Remove(player);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in CleanUpPlayer for player {player.NickName}: {e.Message}");
            }
        }

        public override void OnLeftRoom()
        {
            foreach (var rig in Riglist.Values)
            {
                try
                {
                    if (rig != null)
                    {
                        rig.transform.localScale = Vector3.one;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error resetting rig scale in OnLeftRoom: {e.Message}");
                }
            }

            foreach (var watch in PlayerList.Values)
            {
                try
                {
                    if (watch != null)
                    {
                        Destroy(watch);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error destroying watch in OnLeftRoom: {e.Message}");
                }
            }

            PlayerList.Clear();
            Riglist.Clear();
        }
    }
}