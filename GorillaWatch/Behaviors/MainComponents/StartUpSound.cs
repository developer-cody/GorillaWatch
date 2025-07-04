using UnityEngine;

namespace TheGorillaWatch.Behaviors.MainComponents
{
    internal class StartUpSound : MonoBehaviour
    {
        private StartUpSound() => GorillaTagger.OnPlayerSpawned(() =>
            StartCoroutine(Utils.Instance.PlaySoundFromURL("https://github.com/developer-cody/GorillaWatch/raw/refs/heads/main/Assets/GorillaWatch.wav", 5f, AudioType.WAV)));
    }
}