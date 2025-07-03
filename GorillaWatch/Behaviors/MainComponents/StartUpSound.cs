using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace TheGorillaWatch.Behaviors.MainComponents
{
    internal class StartUpSound : MonoBehaviour
    {
        private void Start()
        {
            GorillaTagger.OnPlayerSpawned(() =>
            {
                StartCoroutine(PlaySound());
            });
        }

        private IEnumerator PlaySound()
        {
            var audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://github.com/developer-cody/GorillaWatch/raw/refs/heads/main/Assets/GorillaWatch.wav", AudioType.WAV))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Failed to load audio clip: {www.error}");
                }
                else
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    audioSource.PlayOneShot(clip, 5);
                }
            }
        }
    }
}