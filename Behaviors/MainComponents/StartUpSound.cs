using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.MainComponents
{
    // I know this is a horrible way of doing... well, everything, but idgaf!
    // I've never needed to make anything play a sound, so I haven't learned.
    // This works, so I'm keeping it.
    internal class StartUpSound : MonoBehaviour
    {
        private void Start()
        {
            var audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TheGorillaWatch.Assets.GorillaWatch.wav");
            byte[] wavData = new byte[stream.Length];
            stream.Read(wavData, 0, wavData.Length);

            var clip = WavToClip(wavData, "GorillaWatch");

            GorillaTagger.OnPlayerSpawned(() => audioSource.PlayOneShot(clip, 5f));
        }

        private AudioClip WavToClip(byte[] wavData, string clipName)
        {
            const int headerSize = 44;
            int channels = BitConverter.ToInt16(wavData, 22);
            int sampleRate = BitConverter.ToInt32(wavData, 24);
            int sampleCount = (wavData.Length - headerSize) / 2;

            float[] samples = new float[sampleCount];
            for (int i = 0; i < sampleCount; i++)
                samples[i] = BitConverter.ToInt16(wavData, headerSize + i * 2) / 32768f;

            var clip = AudioClip.Create(clipName, sampleCount / channels, channels, sampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }
    }
}