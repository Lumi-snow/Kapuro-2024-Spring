namespace AudioController
{
    using System;
    using System.Reflection;
    using UnityEngine;

    public class SEAssistant : AudioAssistant
    {
        [SerializeField] private bool isLoop = false;
        [SerializeField] public bool IsLoop
        {
            get
            {
                return isLoop;
            }
            set
            { 
                isLoop = value;
            }
        }

        /*再生関係*/
        //再生
        public override void Play()
        {
            Play(null);
        }

        //コールバックを指定してSE再生
        public void Play(Action callback)
        {
            if (audioClip == null)
            {
                {
                    Debug.LogWarning(gameObject.name + "のSEAssistantにAudioClipが設定されていません");
                    callback?.Invoke();
                    return;
                }
            }

            SEController.Instance.Play(audioClip, volumeRate, delay, pitch, isLoop, callback);
            if(fadeInDuration > 0)
            {
                SEController.Instance.FadeIn(audioClip.name, fadeInDuration);
            }
        }
    }
}
