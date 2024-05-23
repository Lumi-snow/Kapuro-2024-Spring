namespace AudioController
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class BGMSwitcher
    {
        //�Đ����̂��̂��t�F�[�h�A�E�g�����Ď��̍Đ����J�n����
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

        //�Đ����̂��̂𑦒�~�����āA���̂��̂��t�F�[�h�C���ŊJ�n������
        public static void FadeIn(string audioPath, float fadeInDuration = 1.0f, float volumeRate = 1.0f, float delay = 0.0f, float pitch = 1.0f, bool isLoop = true, Action callback = null)
        {
            BGMController.Instance.Stop();
            BGMController.Instance.Play(audioPath, volumeRate, delay, pitch, isLoop);
            BGMController.Instance.FadeIn(audioPath, fadeInDuration, callback);
        }

        //�Đ����̂��̂��t�F�[�h�A�E�g�����āA���̂��̂��t�F�[�h�C���ŊJ�n����
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

        //�Đ����̂��̂��t�F�[�h�A�E�g�����āA�����Ɏ��̂��̂��t�F�[�h�C���ŊJ�n����
        public static void CrossFade(string audioPath, float fadeDuration = 1.0f, float volumeRate = 1.0f, float delay = 0.0f, float pitch = 1, bool isLoop = true, Action callback = null)
        {
            if(BGMController.Instance.GetCurrentAudioNames().Count >= BGMController.Instance.AudioPlayerNum)
            {
                Debug.LogWarning("�N���X�t�F�[�h����ɂ�Audio Player Num������܂���");
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
