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
        private bool ToggleMod;
        private bool ToggleWatch;
        private int counter;
        private float pageCooldown;

        private void Update()
        {
            if (!ModInitializer.Initialized) return;

            var huntComputer = GorillaTagger.Instance.offlineVRRig.huntComputer.GetComponent<GorillaHuntComputer>();
            var huntWatchComponents = new GameObject[] {
                huntComputer.badge.gameObject,
                huntComputer.leftHand.gameObject,
                huntComputer.rightHand.gameObject,
                huntComputer.hat.gameObject,
                huntComputer.face.gameObject
            };

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED") &&
                !PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("HUNT"))
            {
                reset = false;
                HandleInput();

                foreach (var component in huntWatchComponents) component.SetActive(false);

                foreach (ModPage p in ModInitializer.Mods)
                    if (p.modEnabled && p.pageType == PageType.Toggle)
                        p.OnUpdate();

                GorillaTagger.Instance.offlineVRRig.EnableHuntWatch(watchOn);
                huntComputer.enabled = false;

                if (watchOn)
                    RenderWatch(huntComputer);
            }
            else if (!reset)
            {
                GorillaTagger.Instance.offlineVRRig.EnableHuntWatch(false);
                huntComputer.enabled = false;
                foreach (var component in huntWatchComponents)
                    component.SetActive(false);

                foreach (ModPage mod in ModInitializer.Mods)
                    mod.Disable();

                reset = true;
            }
        }

        private void HandleInput()
        {
            bool useLeftTrigger = ConfigManager.toggleModButton.Value;
            bool useRightTrigger = ConfigManager.toggleWatchButton.Value;

            if (!useLeftTrigger)
            {
                if (ModInitializer.IsSteamVR)
                    ToggleMod = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand);
                else
                    ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out ToggleMod);
            }
            else
            {
                ToggleMod = ControllerInputPoller.instance.leftControllerIndexFloat > 0.5f;
            }

            if (!useRightTrigger)
            {
                if (ModInitializer.IsSteamVR) ToggleWatch = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand);
                else ControllerInputPoller.instance.rightControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out ToggleWatch);
            }
            else
            {
                ToggleWatch = ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f;
            }

            if (ToggleWatch && !stickClickJustPressed && ConfigManager.toggleableWatch.Value)
            {
                watchOn = !watchOn;
                stickClickJustPressed = true;
            }
            else if (!ToggleWatch)
            {
                stickClickJustPressed = false;
            }
        }

        private void RenderWatch(GorillaHuntComputer huntComputer)
        {
            if (ControllerInputPoller.instance.leftControllerSecondaryButton && Time.time > pageCooldown + 0.5f)
            {
                pageCooldown = Time.time;
                counter++;
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 1f);
            }

            if (ControllerInputPoller.instance.leftControllerPrimaryButton && Time.time > pageCooldown + 0.5f)
            {
                pageCooldown = Time.time;
                counter--;
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 1f);
            }

            if (counter < 0) counter = ModInitializer.Mods.Count - 1;
            if (counter >= ModInitializer.Mods.Count) counter = 0;

            ModPage current = ModInitializer.Mods[counter];
            switch (current.pageType)
            {
                case PageType.Toggle:
                    huntComputer.material.gameObject.SetActive(true);
                    string modStatus = current.modEnabled ? "<color=green>enabled</color>" : "<color=red>disabled</color>";
                    huntComputer.text.text = current.modName + ":\n" + modStatus + $"\n{current.info}";
                    huntComputer.material.color = current.modEnabled ? Color.green : Color.red;

                    if (ToggleMod && Time.time > pageCooldown + 0.5f)
                    {
                        pageCooldown = Time.time;
                        ToggleModPage(current);
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, true, 1f);
                    }
                    break;

                case PageType.Information:
                    huntComputer.material.gameObject.SetActive(false);
                    huntComputer.text.text = current.info;
                    break;
            }
        }

        private void ToggleModPage(ModPage page)
        {
            if (page.modEnabled)
            {
                foreach (var mod in ModInitializer.Mods)
                    if (page.requiredModNames.Contains(mod.modName))
                        mod.Disable();
                page.Disable();
            }
            else
            {
                foreach (var mod in ModInitializer.Mods)
                    if (page.incompatibleModNames.Contains(mod.modName))
                        mod.Disable();
                foreach (var mod in ModInitializer.Mods)
                    if (page.requiredModNames.Contains(mod.modName))
                        mod.Enable();
                page.Enable();
            }
        }
    }
}