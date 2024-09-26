using BepInEx;
using System;
using TheGorillaWatch.Patches;
using UnityEngine;
using GorillaNetworking;
using UnityEngine.XR.LegacyInputHelpers;
using Photon.Pun;
using HarmonyLib;
using Valve.VR;
using UnityEngine.XR;
using System.Collections.Generic;
using System.Linq;
using TheGorillaWatch.Models;
using TheGorillaWatch.Mods;

namespace TheGorillaWatch
{

    [BepInPlugin("com.ArtificialGorillas.gorillatag.GorillaWatch", "GorillaWatch", "1.4.5")]
    public class Mod : BaseUnityPlugin
    {
        bool inRoom;

        public static int counter;

        public static float PageCoolDown;

        bool IsSteamVR;

        bool watchOn = true;

        bool stickClickJustPressed;

        public static List<Page> mods = new List<Page>();

        bool reset = true;

        bool initialized = false;

        Vector3 ogGravity;

        void Start()
        {
            ogGravity = Physics.gravity;
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
                            if(modObject.modName == "GorillaWatchMainInfoPageWABSHUWAJSD")
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
            initialized = true;
            foreach (Page page in mods)
            {
                page.Init();
            }
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

                bool leftStickClick;

                bool rightStickClick;

                if (IsSteamVR)
                {
                    rightStickClick = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.RightHand);
                    leftStickClick = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.LeftHand);
                }
                else
                {
                    ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftStickClick);

                    ControllerInputPoller.instance.rightControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightStickClick);
                }

                if (rightStickClick && !stickClickJustPressed)
                {
                    watchOn = !watchOn;
                    stickClickJustPressed = true;
                }
                else if (!rightStickClick)
                {
                    stickClickJustPressed = false;
                }

                GorillaTagger.Instance.offlineVRRig.EnableHuntWatch(watchOn);
                GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>().enabled = false;
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
                            if (leftStickClick && Time.time > PageCoolDown + .5)
                            {
                                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, true, 1f);

                                PageCoolDown = Time.time;
                                if (mods[counter].modEnabled)
                                {
                                    mods[counter].Disable();
                                }
                                else
                                {
                                    mods[counter].Enable();
                                }
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
                Physics.gravity = new Vector3(0f, -9.807f, 0f);
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
