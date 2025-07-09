using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Networking
{
    public class WatchNetworkingManager : MonoBehaviourPunCallbacks
    {
        public Dictionary<Player, GameObject> PlayerList = new Dictionary<Player, GameObject>();
        public Dictionary<Player, GameObject> Riglist = new Dictionary<Player, GameObject>();

        public override void OnJoinedRoom()
        {
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                if (p.CustomProperties.ContainsKey("GorillaWatch"))
                {
                    try
                    {
                        StartCoroutine(WaitForInit(p));
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error starting WaitForInit coroutine for player {p.NickName}: {e.Message}");
                    }
                }
            }
        }

        public IEnumerator WaitForInit(Player p)
        {
            yield return new WaitForSeconds(0.7f);
            InitializePlayerWatch(p);
        }

        public void InitializePlayerWatch(Player p)
        {
            try
            {
                var rig = GorillaGameManager.instance.FindPlayerVRRig(p);
                if (rig == null) return;

                var huntwatch = Instantiate(GorillaTagger.Instance.offlineVRRig.huntComputer);
                huntwatch.transform.SetParent(rig.leftHandTransform, false);
                huntwatch.transform.localPosition = new Vector3(-0.6364f, 0.6427f, 0.0153f);
                huntwatch.transform.localRotation = GorillaTagger.Instance.offlineVRRig.huntComputer.transform.localRotation;

                huntwatch.GetComponent<GorillaHuntComputer>().text.text = "<color=black>Gorilla</colors>Watch";
                huntwatch.GetComponent<GorillaHuntComputer>().text.alignment = TextAnchor.MiddleCenter;

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

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (PlayerList.TryGetValue(otherPlayer, out var playerObject) && Riglist.TryGetValue(otherPlayer, out var rig))
            {
                try
                {
                    Destroy(playerObject);
                    PlayerList.Remove(otherPlayer);
                    Riglist.Remove(otherPlayer);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error cleaning up player {otherPlayer.NickName}: {e.Message}");
                }
            }
        }

        public override void OnLeftRoom()
        {
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