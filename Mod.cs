using BepInEx;
using System;
using TheGorillaWatch.Patches;
using UnityEngine;
using GorillaNetworking;
using GorillaLocomotion;
using CjLib;
using UnityEngine.XR.LegacyInputHelpers;
using System.Collections;
using Photon.Pun;
using HarmonyLib;
using Valve.VR;
using UnityEngine.XR;
using System.Collections.Generic;
using System.Linq;
using TheGorillaWatch.Models;
using TheGorillaWatch.Mods;
using System.Globalization;
using TheGorillaWatch.Configuration;

namespace TheGorillaWatch
{

    [BepInPlugin("com.ArtificialGorillas.gorillatag.GorillaWatch", "GorillaWatch", "1.4.6")]
    public class Mod : BaseUnityPlugin
    {
        bool inRoom;

        public static int counter;

        public static float PageCoolDown;

        bool IsSteamVR;

        //bool ToggleModButton;

        bool watchOn = true;

        bool stickClickJustPressed;

        public static List<Page> mods = new List<Page>();

        bool reset = true;

        bool initialized = false;

        void Start()
        {
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
                            if (modObject.modName == "GorillaWatchMainInfoPage")
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
                    Debug.LogError($"Error With Getting Types: {e}");
                }
            }
            counter = mainPageNum;
        }

        void Initialized()
        {
            IsSteamVR = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";
            foreach (Page page in mods)
            {
                page.Init();
            }
            ConfigManager.CreateConfig();
            initialized = true;
        }

        void Update()
        {
            if (!initialized)
            {
                return;
            }
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED") && !PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("HUNT"))
            {
                reset = false;
                bool stickclick;
                if (IsSteamVR)
                {
                    stickclick = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand);
                }
                else
                {
                    ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out stickclick);
                }

                if (stickclick && !stickClickJustPressed)
                {
                    watchOn = !watchOn;
                    stickClickJustPressed = true;
                }
                else if (!stickclick)
                {
                    stickClickJustPressed = false;
                }

                bool ModToggleButton;

                bool ConfigManagerValue;

                ConfigManagerValue = ConfigManager.toggleButton.Value == true;

                if (IsSteamVR)
                {
                    if (ConfigManagerValue)
                    {
                        ModToggleButton = ControllerInputPoller.instance.leftControllerIndexTouch > 0.3f;
                    }
                    else
                    {
                        ModToggleButton = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand);
                    }
                }
                else
                {
                    if (ConfigManagerValue)
                    {
                        ModToggleButton = ControllerInputPoller.instance.leftControllerIndexTouch > 0.3f;
                    }
                    else
                    {
                        ModToggleButton = ControllerInputPoller.instance.rightControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out ModToggleButton);
                    }
                }

                GorillaTagger.Instance.offlineVRRig.EnableHuntWatch(watchOn);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().enabled = false;
                //GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().material.gameObject.SetActive(false);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().badge.gameObject.SetActive(false);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().leftHand.gameObject.SetActive(false);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().rightHand.gameObject.SetActive(false);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().hat.gameObject.SetActive(false);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().face.gameObject.SetActive(false);

                foreach (Page p in mods)
                {
                    if (p.modEnabled && p.pageType == PageType.Toggle)
                    {
                        p.OnUpdate();
                    }
                }

                if (watchOn)
                {
                    if (ControllerInputPoller.instance.leftControllerPrimaryButton && Time.time > PageCoolDown + 0.5)
                    {
                        PageCoolDown = Time.time;
                        counter++;
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 1f);
                    }
                    if (ControllerInputPoller.instance.leftControllerSecondaryButton && Time.time > PageCoolDown + 0.5f)
                    {
                        PageCoolDown = Time.time;
                        counter--;
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 1f);
                    }
                    if (counter < 0)
                    {
                        counter = mods.Count - 1;
                    }
                    if (counter >= mods.Count)
                    {
                        counter = 0;
                    }
                    switch (mods[counter].pageType)
                    {
                        case PageType.Toggle:
                            GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().material.gameObject.SetActive(true);
                            string modEnabled = mods[counter].modEnabled ? "<color=green>enabled</color>" : $"<color=red>disabled</color>";
                            GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().text.text = mods[counter].modName + ":\n" + modEnabled + $"\n{mods[counter].info}";
                            GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().material.color = mods[counter].modEnabled ? Color.green : Color.red;
                            if (ModToggleButton && Time.time > PageCoolDown + .5)
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
                            GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().material.gameObject.SetActive(false);
                            GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().text.text = mods[counter].info;
                            break;
                    }
                }
            }
            else if (!reset)
            {
                foreach (Page mod in mods)
                {
                    mod.Disable();
                }
                GorillaTagger.Instance.offlineVRRig.EnableHuntWatch(false);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().enabled = true;
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().material.gameObject.SetActive(true);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().badge.gameObject.SetActive(true);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().leftHand.gameObject.SetActive(true);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().rightHand.gameObject.SetActive(true);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().hat.gameObject.SetActive(true);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().face.gameObject.SetActive(true);
                reset = true;
            }
        }
    }
}
