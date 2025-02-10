using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using TheGorillaWatch.Configuration;
using TheGorillaWatch.Models;
using TheGorillaWatch.Patches;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace TheGorillaWatch
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Mod : BaseUnityPlugin
    {
        bool IsSteamVR;
        bool watchOn = true;
        bool stickClickJustPressed;
        bool reset = true;
        bool initialized;
        bool useLeftTriggerToToggleMod;
        bool useRightTriggerToToggleWatch;
        bool toggleableWatch;
        bool ToggleMod;
        bool ToggleWatch;
        public static int counter;
        public static float PageCoolDown;
        public static List<Page> mods = new List<Page>();

        void Start()
        {
            ConfigManager.CreateConfig();
            GorillaTagger.OnPlayerSpawned(Initialized);
            GameObject modHolder = new GameObject("GorillaWatch Mod Holder");
            int mainPageNum = 0;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Page)));
                    foreach (var type in types)
                    {
                        try
                        {
                            Debug.Log(type.Name);
                            GameObject mod = new GameObject($"Mod {type.Name}");
                            Page modObject = (Page)mod.AddComponent(type);
                            mods.Add(modObject);
                            mod.transform.SetParent(modHolder.transform);
                            if (modObject.modName == "GorillaWatchMainInfoPageWABSHUWAJSD")
                            {
                                mainPageNum = mods.IndexOf(modObject);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"Error adding mod {type.Name}: {e}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error With Getting Types: {e.Message}!");
                }
            }
            counter = mainPageNum;
        }

        void Initialized()
        {
            IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";
            initialized = true;

            var hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add(Constants.Name, Constants.Version);
            hash.Add("size", 1f);
            PhotonNetwork.LocalPlayer.CustomProperties = hash;
            PhotonNetwork.SetPlayerCustomProperties(hash);
            gameObject.AddComponent<NetworkingManager>();

            foreach (Page page in mods)
            {
                page.Init();
            }

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void Update()
        {
            var huntComputer = GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>();
            var huntWatchComponents = new GameObject[] {
                    huntComputer.badge.gameObject,
                    huntComputer.leftHand.gameObject,
                    huntComputer.rightHand.gameObject,
                    huntComputer.hat.gameObject,
                    huntComputer.face.gameObject
            };

            if (!initialized) return;

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED") && !PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("HUNT"))
            {
                reset = false;

                useLeftTriggerToToggleMod = ConfigManager.toggleModButton.Value;
                useRightTriggerToToggleWatch = ConfigManager.toggleWatchButton.Value;

                if (!useLeftTriggerToToggleMod)
                {
                    if (IsSteamVR) { ToggleMod = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand); }
                    else { ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out ToggleMod); }
                }
                else
                {
                    ToggleMod = ControllerInputPoller.instance.leftControllerIndexFloat > 0.5f;
                }

                if (!useRightTriggerToToggleWatch)
                {
                    if (IsSteamVR) { ToggleWatch = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand); }
                    else { ControllerInputPoller.instance.rightControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out ToggleWatch); }
                }
                else
                {
                    ToggleWatch = ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f;
                }

                if (ToggleWatch && !stickClickJustPressed && toggleableWatch) { watchOn = !watchOn; stickClickJustPressed = true; }
                else if (!ToggleWatch) { stickClickJustPressed = false; }

                foreach (var component in huntWatchComponents)
                {
                    component.SetActive(false);
                }

                foreach (Page p in mods)
                {
                    if (p.modEnabled && p.pageType == PageType.Toggle)
                    {
                        p.OnUpdate();
                    }
                }

                toggleableWatch = ConfigManager.toggleableWatch.Value;
                GorillaTagger.Instance.offlineVRRig.EnableHuntWatch(watchOn);

                huntComputer.enabled = false;

                if (watchOn)
                {
                    if (ControllerInputPoller.instance.leftControllerSecondaryButton && Time.time > PageCoolDown + 0.5)
                    {
                        PageCoolDown = Time.time;
                        counter++;
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 1f);
                    }

                    if (ControllerInputPoller.instance.leftControllerPrimaryButton && Time.time > PageCoolDown + 0.5f)
                    {
                        PageCoolDown = Time.time;
                        counter--;
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 1f);
                    }

                    if (counter < 0) { counter = mods.Count - 1; }
                    if (counter >= mods.Count) { counter = 0; }

                    switch (mods[counter].pageType)
                    {
                        case PageType.Toggle:
                            huntComputer.material.gameObject.SetActive(true);
                            string modEnabled = mods[counter].modEnabled ? "<color=green>enabled</color>" : $"<color=red>disabled</color>";
                            huntComputer.text.text = mods[counter].modName + ":\n" + modEnabled + $"\n{mods[counter].info}";
                            huntComputer.material.color = mods[counter].modEnabled ? Color.green : Color.red;

                            if (ToggleMod && Time.time > PageCoolDown + .5)
                            {
                                PageCoolDown = Time.time;
                                if (mods[counter].modEnabled)
                                {
                                    if (mods[counter].requiredModNames.Count > 0)
                                    {
                                        foreach (Page mod in mods)
                                        {
                                            if (mods[counter].requiredModNames.Contains(mod.modName))
                                            {
                                                mod.Disable();
                                            }
                                        }
                                    }
                                    mods[counter].Disable();
                                }
                                else
                                {
                                    if (mods[counter].incompatibleModNames.Count > 0)
                                    {
                                        foreach (Page mod in mods)
                                        {
                                            if (mods[counter].incompatibleModNames.Contains(mod.modName))
                                            {
                                                mod.Disable();
                                            }
                                        }
                                    }

                                    if (mods[counter].requiredModNames.Count > 0)
                                    {
                                        foreach (Page mod in mods)
                                        {
                                            if (mods[counter].requiredModNames.Contains(mod.modName))
                                            {
                                                mod.Enable();
                                            }
                                        }
                                    }
                                    mods[counter].Enable();
                                }
                                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, true, 1f);
                            }
                            break;

                        case PageType.Information:
                            huntComputer.material.gameObject.SetActive(false);
                            huntComputer.text.text = mods[counter].info;
                            break;

                        case PageType.notatogglebutnotinfo:
                            huntComputer.text.text = mods[counter].modName + "\n" + mods[counter].info;
                            huntComputer.material.gameObject.SetActive(false);

                            if (ToggleMod)
                            {
                                mods[counter].OnUpdate();
                            }
                            break;
                    }
                }
            }
            else if (!reset)
            {
                GorillaTagger.Instance.offlineVRRig.EnableHuntWatch(false);
                huntComputer.enabled = false;
                foreach (var component in huntWatchComponents)
                {
                    component.SetActive(false);
                }

                foreach (Page mod in mods) { mod.Disable(); }
                reset = true;
            }
        }
    }
}