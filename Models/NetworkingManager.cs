using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TheGorillaWatch.Models
{
    public class NetworkingManager : MonoBehaviourPunCallbacks
    {
        //striker did this meanie heads

        //fuk u striker && love ya ty :3

        //:3

        //:3

        //:3

        //:3

        //:3

        public Dictionary<Player, GameObject> PlayerList = new Dictionary<Player, GameObject>();
        public Dictionary<Player, GameObject> Riglist = new Dictionary<Player, GameObject>();

        public override void OnJoinedRoom()
        {
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                if (p.CustomProperties.ContainsKey("GorillaWatch"))
                {
                    StartCoroutine("WaitForInit", p);
                }
            }
        }

        IEnumerator WaitForInit(Player p)
        {
            yield return new WaitForSeconds(0.7f);
            var rig = GorillaGameManager.instance.FindPlayerVRRig(p);
            var huntwatch = GameObject.Instantiate(GorillaTagger.Instance.offlineVRRig.huntComputer);
            huntwatch.transform.parent = rig.leftHandTransform;
            huntwatch.transform.localPosition = new Vector3(-0.5737f, 0.5827f, 0.0353f);
            huntwatch.transform.localRotation = GorillaTagger.Instance.offlineVRRig.huntComputer.transform.localRotation;
            PlayerList.Add(p, huntwatch);
            Riglist.Add(p, rig.gameObject);
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (newPlayer.CustomProperties.ContainsKey("GorillaWatch"))
            {
                var rig = GorillaGameManager.instance.FindPlayerVRRig(newPlayer);
                var huntwatch = GameObject.Instantiate(GorillaTagger.Instance.offlineVRRig.huntComputer);
                huntwatch.transform.parent = rig.leftHandTransform;
                huntwatch.transform.localPosition = new Vector3(-0.5737f, 0.5827f, 0.0353f);
                huntwatch.transform.localRotation = GorillaTagger.Instance.offlineVRRig.huntComputer.transform.localRotation;
                PlayerList.Add(newPlayer, huntwatch);
                Riglist.Add(newPlayer, rig.gameObject);
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            try
            {
                if (changedProps.ContainsKey("size") && !targetPlayer.IsLocal)
                {
                    var rig = GorillaGameManager.instance.FindPlayerVRRig(targetPlayer);
                    rig.transform.localScale = new Vector3((float)changedProps["size"], (float)changedProps["size"], (float)changedProps["size"]);
                }
            }
            catch (Exception e)
            {
                Debug.Log(targetPlayer.NickName);
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (PlayerList.ContainsKey(otherPlayer))
            {
                Riglist[otherPlayer].transform.localScale = new Vector3(1f, 1f, 1f);
                Destroy(PlayerList[otherPlayer].gameObject);
                PlayerList.Remove(otherPlayer);
                Riglist.Remove(otherPlayer);
            }
        }

        public override void OnLeftRoom()
        {
            foreach (Player rig in Riglist.Keys)
            {
                Riglist[rig].transform.localScale = new Vector3(1f, 1f, 1f);
            }
            foreach (GameObject Watch in PlayerList.Values)
            {
                Destroy(Watch);
            }
            PlayerList.Clear();
            Riglist.Clear();
        }
    }
}