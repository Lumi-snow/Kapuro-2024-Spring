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

        /*�Đ��֌W*/
        //�Đ�
        public override void Play()
        {
            Play(null);
        }

        //�R�[���o�b�N���w�肵��SE�Đ�
        public void Play(Action callback)
        {
            if (audioClip == null)
            {
                {
                    Debug.LogWarning(gameObject.name + "��SEAssistant��AudioClip���ݒ肳��Ă��܂���");
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
