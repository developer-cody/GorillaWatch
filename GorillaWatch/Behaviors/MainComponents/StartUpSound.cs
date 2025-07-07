using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

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

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Failed to load audio clip: {www.error}");
                }
                else
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    audioSource.PlayOneShot(clip, 5f);
                }
            }
        }
    }
}