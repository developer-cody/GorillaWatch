using System;
using System.Collections.Generic;
using System.Linq;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.MainComponents
{
    public class ModInitializer : MonoBehaviour
    {
        public static bool IsSteamVR;
        public static bool Initialized;
        public static List<ModPage> Mods = new List<ModPage>();
        public static int MainPageIndex;

        private void Start() => GorillaTagger.OnPlayerSpawned(() =>
        {
            IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";
            Initialized = true;

            var hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add(Constants.NAME, Constants.VERS);
            hash.Add("size", 1f);
            PhotonNetwork.LocalPlayer.CustomProperties = hash;
            PhotonNetwork.SetPlayerCustomProperties(hash);
            gameObject.AddComponent<NetworkingManager>();

            GameObject modHolder = new GameObject("GorillaWatch Mod Holder");

            foreach (string modName in ModOrder.OrderedModNames)
            {
                bool found = false;

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        var type = assembly.GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(ModPage)) && t.Name == modName);
                        if (type != null)
                        {
                            GameObject modObj = new GameObject($"Mod {type.Name}");
                            ModPage page = (ModPage)modObj.AddComponent(type);
                            Mods.Add(page);
                            modObj.transform.SetParent(modHolder.transform);
                            found = true;
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error loading mod {modName} in assembly {assembly.FullName}: {e}");
                    }
                }

                if (!found)
                {
                    Debug.LogWarning($"ModPage not found for: {modName}");
                }
            }

            foreach (var page in Mods)
            {
                page.Init();
            }
        });
    }
}