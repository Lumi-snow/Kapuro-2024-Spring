namespace AudioController
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class BGMSwitcher
    {
        //再生中のものをフェードアウトさせて次の再生を開始する
        public static void FadeOut(string audioPath, float fadeOutDuration = 1.0f, float volumeRate = 1.0f, float delay = 0.0f, float pitch = 1.0f, bool isLoop = true, Action callback = null)
        {
            if (BGMController.Instance.IsPlaying() == false)
            {
                BGMController.Instance.Play(audioPath, volumeRate, delay, pitch, isLoop);
                return;
            }

            BGMController.Instance.FadeOut(fadeOutDuration, () => {
                BGMController.Instance.Play(audioPath, volumeRate, delay, pitch, isLoop);
                callback?.Invoke();
            });
        }

        //再生中のものを即停止させて、次のものをフェードインで開始させる
        public static void FadeIn(string audioPath, float fadeInDuration = 1.0f, float volumeRate = 1.0f, float delay = 0.0f, float pitch = 1.0f, bool isLoop = true, Action callback = null)
        {
            BGMController.Instance.Stop();
            BGMController.Instance.Play(audioPath, volumeRate, delay, pitch, isLoop);
            BGMController.Instance.FadeIn(audioPath, fadeInDuration, callback);
        }

        //再生中のものをフェードアウトさせて、次のものをフェードインで開始する
        public static void FadeOutAndFadeIn(string audioPath, float fadeOutDuration = 1.0f, float fadeInDuration = 1.0f, float volumeRate = 1.0f, float delay = 0.0f, float pitch = 1.0f, bool isLoop = true, Action callback = null)
        {
            if(BGMController.Instance.IsPlaying() == false)
            {
                FadeIn(audioPath, fadeInDuration, volumeRate, delay, pitch, isLoop, callback);
                return;
            }

            BGMController.Instance.FadeOut(fadeOutDuration, () =>
            {
                FadeIn(audioPath, fadeInDuration, volumeRate, delay, pitch, isLoop, callback);
            });
        }

        //再生中のものをフェードアウトさせて、同時に次のものをフェードインで開始する
        public static void CrossFade(string audioPath, float fadeDuration = 1.0f, float volumeRate = 1.0f, float delay = 0.0f, float pitch = 1, bool isLoop = true, Action callback = null)
        {
            if(BGMController.Instance.GetCurrentAudioNames().Count >= BGMController.Instance.AudioPlayerNum)
            {
                Debug.LogWarning("クロスフェードするにはAudio Player Numが足りません");
            }

            foreach(var currentAudioName in BGMController.Instance.GetCurrentAudioNames())
            {
                BGMController.Instance.FadeOut(currentAudioName, fadeDuration);
            }

            BGMController.Instance.Play(audioPath, volumeRate, delay, pitch, isLoop, allowsDuplicate: true);
            BGMController.Instance.FadeIn(audioPath, fadeDuration, callback);
        }
    }
}
