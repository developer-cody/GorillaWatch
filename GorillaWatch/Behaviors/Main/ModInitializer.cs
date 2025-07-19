using System;
using System.Linq;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using TheGorillaWatch.Behaviors.Networking;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;
using Logger = TheGorillaWatch.Utilities.Logger;

namespace TheGorillaWatch.Behaviors.MainComponents
{
    public class ModInitializer : MonoBehaviour
    {
        public static bool IsSteamVR;
        public static bool Initialized;
        public static readonly System.Collections.Generic.List<ModPage> Mods = new System.Collections.Generic.List<ModPage>();
        public static int MainPageIndex;

        private void Start()
        {
            GorillaTagger.OnPlayerSpawned(() =>
            {
                IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";
                Initialized = true;

                var hash = new ExitGames.Client.Photon.Hashtable
                {
                    { Constants.NAME, Constants.VERS },
                    { "size", 1f }
                };
                PhotonNetwork.LocalPlayer.CustomProperties = hash;
                PhotonNetwork.SetPlayerCustomProperties(hash);

                gameObject.AddComponent<WatchNetworkingManager>();
                gameObject.AddComponent<SizeNetworkingManager>();

                var modHolder = new GameObject("GorillaWatch Mod Holder");

                foreach (string modName in ModOrder.OrderedModNames)
                {
                    try
                    {
                        var type = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .FirstOrDefault(t => t.IsSubclassOf(typeof(ModPage)) && t.Name == modName);

                        if (type != null)
                        {
                            var modObj = new GameObject($"Mod {type.Name}");
                            var page = (ModPage)modObj.AddComponent(type);
                            Mods.Add(page);
                            modObj.transform.SetParent(modHolder.transform);
                        }
                        else
                        {
                            Logger.Warn($"ModPage not found", modName);
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"Error loading mod {modName}: {e}");
                    }
                }

                foreach (var page in Mods) page.Init();
            });
        }
    }
}