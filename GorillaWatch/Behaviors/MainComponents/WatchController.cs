using Photon.Pun;
using TheGorillaWatch.Behaviors.Page;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace TheGorillaWatch.Behaviors.MainComponents
{
    public class WatchController : MonoBehaviour
    {
        private bool watchOn = true;
        private bool stickClickJustPressed;
        private bool reset = true;
        private int pageIndex;
        private float pageCooldown;
        private readonly GameObject[] huntWatchComponents = new GameObject[5];

        private void Start() => GorillaTagger.OnPlayerSpawned(() => {
            var huntComputer = GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>();

            huntWatchComponents[0] = huntComputer.badge.gameObject;
            huntWatchComponents[1] = huntComputer.leftHand.gameObject;
            huntWatchComponents[2] = huntComputer.rightHand.gameObject;
            huntWatchComponents[3] = huntComputer.hat.gameObject;
            huntWatchComponents[4] = huntComputer.face.gameObject;
        });

        private void Update()
        {
            if (!ModInitializer.Initialized) return;

            var huntComputer = GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>();
            bool InModdedRoom = PhotonNetwork.InRoom &&
                PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("gameMode", out var gameMode) &&
                gameMode.ToString().Contains("MODDED") &&
                !gameMode.ToString().Contains("HUNT");

            if (InModdedRoom)
            {
                reset = false;
                bool toggleMod = ConfigManager.toggleModButton.Value
                    ? ControllerInputPoller.instance.leftControllerIndexFloat > 0.5f
                    : ModInitializer.IsSteamVR
                        ? SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand)
                        : ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool modResult) && modResult;

                bool toggleWatch = ConfigManager.toggleWatchButton.Value
                    ? ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f
                    : ModInitializer.IsSteamVR
                        ? SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand)
                        : ControllerInputPoller.instance.rightControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool watchResult) && watchResult;

                if (toggleWatch && !stickClickJustPressed && ConfigManager.toggleableWatch.Value)
                {
                    watchOn = !watchOn;
                    stickClickJustPressed = true;
                }
                else if (!toggleWatch)
                {
                    stickClickJustPressed = false;
                }

                foreach (var component in huntWatchComponents) component.SetActive(false);
                foreach (var mod in ModInitializer.Mods) if (mod.modEnabled && mod.pageType == PageType.Toggle) mod.OnUpdate();

                GorillaTagger.Instance.offlineVRRig.EnableHuntWatch(watchOn);
                huntComputer.enabled = false;

                if (!watchOn) return;

                if (ControllerInputPoller.instance.leftControllerSecondaryButton && Time.time > pageCooldown + 0.5f)
                {
                    pageCooldown = Time.time;
                    pageIndex = (pageIndex + 1) % ModInitializer.Mods.Count;
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 1f);
                }

                if (ControllerInputPoller.instance.leftControllerPrimaryButton && Time.time > pageCooldown + 0.5f)
                {
                    pageCooldown = Time.time;
                    pageIndex = pageIndex == 0 ? ModInitializer.Mods.Count - 1 : pageIndex - 1;
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 1f);
                }

                var current = ModInitializer.Mods[pageIndex];
                huntComputer.material.gameObject.SetActive(current.pageType == PageType.Toggle);
                huntComputer.text.text = current.pageType == PageType.Toggle
                    ? $"{current.modName}:\n{(current.modEnabled ? "<color=green>enabled</color>" : "<color=red>disabled</color>")}\n{current.info}"
                    : current.info;
                huntComputer.material.color = current.modEnabled ? Color.green : Color.red;

                if (toggleMod && current.pageType == PageType.Toggle && Time.time > pageCooldown + 0.5f)
                {
                    pageCooldown = Time.time;
                    if (current.modEnabled)
                    {
                        foreach (var mod in ModInitializer.Mods) if (current.requiredModNames.Contains(mod.modName)) mod.Disable();
                        current.Disable();
                    }
                    else
                    {
                        foreach (var mod in ModInitializer.Mods) if (current.incompatibleModNames.Contains(mod.modName)) mod.Disable();
                        foreach (var mod in ModInitializer.Mods) if (current.requiredModNames.Contains(mod.modName)) mod.Enable();
                        current.Enable();
                    }
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, true, 1f);
                }
            }
            else if (!reset)
            {
                GorillaTagger.Instance.offlineVRRig.EnableHuntWatch(false);
                huntComputer.enabled = false;

                foreach (var component in huntWatchComponents) component.SetActive(false);
                foreach (var mod in ModInitializer.Mods) mod.Disable();

                reset = true;
            }
        }
    }
}