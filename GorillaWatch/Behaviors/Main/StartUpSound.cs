using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Logger = TheGorillaWatch.Utilities.Logger;

namespace TheGorillaWatch.Behaviors.MainComponents
{
    internal class StartUpSound : MonoBehaviour
    {
        private void Start() => GorillaTagger.OnPlayerSpawned(() => _ = PlayStartUpSound());

        private async Task PlayStartUpSound()
        {
            var audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://github.com/developer-cody/GorillaWatch/raw/refs/heads/main/Assets/GorillaWatch.wav", AudioType.WAV))
            {
                var request = www.SendWebRequest();

                while (!request.isDone) await Task.Yield();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    audioSource.PlayOneShot(clip, 5f);
                }
                else Logger.Error($"Failed to load audio clip: {www.error}");
            }
        }
    }
}