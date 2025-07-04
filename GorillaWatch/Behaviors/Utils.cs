using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace TheGorillaWatch.Behaviors
{
    internal class Utils : MonoBehaviour
    {
        public static Utils Instance;

        private void Start() => Instance = this;

        public IEnumerator PlaySoundFromURL(string URL, float Volume, AudioType type)
        {
            var audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(URL, type))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Failed to load audio clip: {www.error}");
                }
                else
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    audioSource.PlayOneShot(clip, Volume);
                }
            }
        }

        public static AssetBundle LoadAssetBundleFromURL(string url)
        {
            try
            {
                using UnityWebRequest unityWebRequest = UnityWebRequestAssetBundle.GetAssetBundle(url);
                unityWebRequest.SendWebRequest();
                while (!unityWebRequest.isDone)
                    Thread.Sleep(10);

                if (unityWebRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Failed to download asset bundle: {unityWebRequest.error}");
                    return null;
                }

                return DownloadHandlerAssetBundle.GetContent(unityWebRequest);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error loading asset bundle from URL: " + ex.Message);
                return null;
            }
        }
    }
}
